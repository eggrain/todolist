namespace todolist.Pages.Goals;

public class ShowModel(AppDbContext db, UserManager<AppUser> users)
                    : AppPageModel(db, users)
{
    public Goal Goal { get; private set; } = null!;
    public Todo NewTodo { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = _users.GetUserId(User)!;

        Goal? goal = await _db.Goals
                .Include(g => g.ProgressNotes.OrderByDescending(n => n.CreatedAt))
                .Include(g => g.Todos.OrderBy(t => t.Completed))
                .FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
        if (goal == null) return NotFound();

        Goal = goal;
        NewTodo = new()
        {
            GoalId = goal.Id,
            UserId = userId
        };

        return Page();
    }
}