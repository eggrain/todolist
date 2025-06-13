namespace todolist.Pages.Goals;

public class NewModel(AppDbContext db, UserManager<AppUser> users)
                                            : AppPageModel(db, users)
{
    [BindProperty]
    public Goal Goal { get; set; } = null!;

    public IActionResult OnGet()
    {
        string userId = _users.GetUserId(User)!;

        Goal = new() { UserId = userId };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        _db.Goals.Add(Goal);
        await _db.SaveChangesAsync();

        return LocalRedirect("/Goals/Index");
    }
}