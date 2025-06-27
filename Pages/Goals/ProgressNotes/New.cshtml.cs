namespace todolist.Pages.Goals.ProgressNotes;

public class NewModel(AppDbContext db, UserManager<AppUser> users)
            : AppPageModel(db, users)
{
    [BindProperty]
    public ProgressNote Note { get; set; } = null!;

    public Goal Goal { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string goalId)
    {
        string userId = UserId();

        Goal? goal = await _db.Goals
            .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);
        if (goal == null) return NotFound();
        Goal = goal;

        Note = new() { GoalId = goalId, UserId = userId };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string goalId)
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync(goalId);
            return Page();
        }

        string userId = UserId();
        if (Note.UserId != userId) return BadRequest();

        Goal? goal = await _db.Goals.FirstOrDefaultAsync(
            g => g.Id == Note.GoalId && g.UserId == userId);
        if (goal == null) return BadRequest();

        _db.ProgressNotes.Add(Note);
        await _db.SaveChangesAsync();

        return LocalRedirect($"/Goals/Show/{Note.GoalId}");
    }
}