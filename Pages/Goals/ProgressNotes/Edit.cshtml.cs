namespace todolist.Pages.Goals.ProgressNotes;

public class EditModel(AppDbContext db, UserManager<AppUser> users)
            : AppPageModel(db, users)
{
    [BindProperty]
    public ProgressNote Note { get; set; } = null!;

    public Goal Goal { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string goalId, string noteId)
    {
        string userId = UserId();

        Goal? goal = await _db.Goals.Include(g => g.ProgressNotes)
            .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);
        if (goal == null) return NotFound();
        Goal = goal;

        ProgressNote? note = goal.ProgressNotes
                    .FirstOrDefault(pn => pn.Id == noteId && pn.UserId == userId);
        if (note == null) return NotFound();

        Note = note;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string goalId, string noteId)
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync(goalId, noteId);
            return Page();
        }

        string userId = UserId();
        if (Note.UserId != userId) return BadRequest();

        ProgressNote? note = await _db.ProgressNotes.FirstOrDefaultAsync(
            pn => pn.Id == noteId && pn.UserId == userId);
        if (note == null) return BadRequest();
        note.Text = Note.Text;

        await _db.SaveChangesAsync();

        return LocalRedirect($"/Goals/Show/{Note.GoalId}");
    }

    public async Task<IActionResult> OnPostDeleteAsync(string goalId, string noteId)
    {
        string userId = UserId();

        ProgressNote? note = await _db.ProgressNotes.FirstOrDefaultAsync(
            pn => pn.Id == noteId && pn.UserId == userId);
        if (note == null) return BadRequest();

        _db.ProgressNotes.Remove(note);
        await _db.SaveChangesAsync();

        return LocalRedirect($"/Goals/Show/{Note.GoalId}");
    }
}
