namespace todolist.Pages.Checklists.ChecklistTasks;

public class EditModel(AppDbContext db, UserManager<AppUser> users)
            : AppPageModel(db, users)
{
    [BindProperty]
    public ChecklistTask Task { get; set; } = null!;

    public Checklist Checklist { get; set; } = null!;

    [BindProperty] public string? ReturnUrl { get; set; }

    public async Task<IActionResult> OnGetAsync(string checklistId, string taskId)
    {
        string userId = UserId();

        Checklist? checklist = await _db.Checklists.Include(g => g.Tasks)
            .FirstOrDefaultAsync(cl => cl.Id == checklistId && cl.UserId == userId);
        if (checklist == null) return NotFound();
        Checklist = checklist;

        ChecklistTask? task = checklist.Tasks
                    .FirstOrDefault(task => task.Id == taskId && task.UserId == userId);
        if (task == null) return NotFound();

        Task = task;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string checklistId, string taskId)
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync(checklistId, taskId);
            return Page();
        }

        string userId = UserId();
        if (Task.UserId != userId) return BadRequest();

        ChecklistTask? task = await _db.ChecklistTasks.FirstOrDefaultAsync(
            task => task.Id == taskId && task.UserId == userId);
        if (task == null) return BadRequest();
        task.Description = Task.Description;

        await _db.SaveChangesAsync();

        return LocalRedirect($"/Checklists/Show/{checklistId}");
    }

    public async Task<IActionResult> OnPostToggleCompleteAsync(string checklistId, string taskId)
    {
        string userId = UserId();

        ChecklistTask? task = await _db.ChecklistTasks.FirstOrDefaultAsync(
            task => task.Id == taskId && task.UserId == userId);
        if (task == null) return BadRequest();

        task.Complete = !task.Complete;

        await _db.SaveChangesAsync();

        if (ReturnUrl != null)
            return LocalRedirect(ReturnUrl);
        return LocalRedirect($"/Checklists/Show/{checklistId}");
    }
}
