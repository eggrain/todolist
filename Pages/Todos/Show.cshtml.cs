namespace todolist.Pages.Todos;

public class ShowModel(AppDbContext db, UserManager<AppUser> users)
                : AppPageModel(db, users)
{
    [BindProperty]
    public Todo Todo { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = UserId();

        Todo? todo = await _db.Todos
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        if (todo == null) return NotFound();

        Todo = todo;
        return Page();
    }                    
}