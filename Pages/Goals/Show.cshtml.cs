namespace todolist.Pages.Goals;

public class ShowModel(AppDbContext db, UserManager<AppUser> users)
                    : AppPageModel(db, users)
{
    [BindProperty]
    public Goal Goal { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = _users.GetUserId(User)!;

        Goal? goal = await _db.Goals
                .FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
        if (goal == null) return NotFound();

        Goal = goal;

        return Page();
    }
}