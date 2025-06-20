namespace todolist.Pages.Todos;

public class DeleteModel(AppDbContext db, UserManager<AppUser> users)
                                            : AppPageModel(db, users)
{
    [BindProperty]
    public string? ReturnUrl { get; set;  }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        string userId = UserId();

        Todo? todo = await _db.Todos
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        if (todo == null) return NotFound();

        _db.Todos.Remove(todo);
        await _db.SaveChangesAsync();

        if (ReturnUrl != null)
            return LocalRedirect(ReturnUrl);

        return LocalRedirect("/Todos/Index");
    }
}
