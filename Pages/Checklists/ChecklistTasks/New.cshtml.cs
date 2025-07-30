namespace todolist.Pages.Checklists.ChecklistTasks;

public class NewModel(AppDbContext db, UserManager<AppUser> users)
            : AppPageModel(db, users)
{
    [BindProperty]
    public ChecklistTask Task { get; set; } = null!;

    public Checklist Checklist { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string checklistId)
    {
        string userId = UserId();

        Checklist? checklist = await _db.Checklists
            .FirstOrDefaultAsync(cl => cl.Id == checklistId && cl.UserId == userId);
        if (checklist == null) return NotFound();
        Checklist = checklist;

        Task = new() { ChecklistId = checklistId, UserId = userId };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string checklistId)
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync(checklistId);
            return Page();
        }

        string userId = UserId();
        if (Task.UserId != userId) return BadRequest();

        Checklist? checklist = await _db.Checklists.Include(cl => cl.Tasks).
            FirstOrDefaultAsync(cl => cl.Id == checklistId && cl.UserId == userId);
        if (checklist == null) return BadRequest();

        Task.Order = checklist.Tasks.Count;

        _db.ChecklistTasks.Add(Task);
        await _db.SaveChangesAsync();

        return LocalRedirect($"/Checklists/Show/{checklistId}");
    }
}
