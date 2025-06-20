namespace todolist.Pages.Todos;

public class EditModel(AppDbContext db, UserManager<AppUser> users)
                : AppPageModel(db, users)
{
    [BindProperty]
    public Todo Todo { get; set; } = null!;

    [BindProperty]
    public string? ReturnUrl { get; set; } = null;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = UserId();
        Todo? todo = await _db.Todos
                .Where(t => t.Id == id && t.UserId == userId).FirstOrDefaultAsync();
        if (todo == null) return NotFound();

        Todo = todo;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (!ModelState.IsValid) return Page();
        string userId = UserId();

        Todo? todo = await _db.Todos
            .Where(t => t.Id == id && t.UserId == userId).FirstOrDefaultAsync();
        if (todo == null) return NotFound();

        todo.Description = Todo.Description;
        todo.OnDate = Todo.OnDate;
        todo.AtTime = Todo.AtTime;
        todo.Completed = Todo.Completed;

        await _db.SaveChangesAsync();

        if (ReturnUrl != null)
            return LocalRedirect(ReturnUrl);

        return LocalRedirect($"/Todos/Show/{id}");
    }
}
