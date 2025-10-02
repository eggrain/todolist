namespace todolist.Pages.Checklists;

public class ShowModel(AppDbContext db, UserManager<AppUser> users)
                    : AppPageModel(db, users)
{
    public Checklist Checklist { get; private set; } = null!;
    public ChecklistTask NewTask { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = UserId();

        Checklist? checklist = await _db.Checklists
                .Include(g => g.Tasks.OrderBy(t => t.Order))
                .FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
        if (checklist == null) return NotFound();

        Checklist = checklist;
        NewTask = new()
        {
            ChecklistId = Checklist.Id,
            UserId = userId
        };

        return Page();
    }

    public async Task<IActionResult> OnPostResetAsync(string id)
    {
        string userId = UserId();

        bool ownsChecklist = await _db.Checklists.AnyAsync(c => c.Id == id && c.UserId == userId);
        if (!ownsChecklist) return NotFound();

        await _db.ChecklistTasks
            .Where(t => t.ChecklistId == id && t.UserId == userId)
            .ExecuteUpdateAsync(updates => updates.SetProperty(t => t.Complete, false));

        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostCompleteAsync(string id)
    {
        string userId = UserId();

        bool ownsChecklist = await _db.Checklists.AnyAsync(c => c.Id == id && c.UserId == userId);
        if (!ownsChecklist) return NotFound();

        await _db.ChecklistTasks
            .Where(t => t.ChecklistId == id && t.UserId == userId)
            .ExecuteUpdateAsync(updates => updates.SetProperty(t => t.Complete, true));

        return RedirectToPage(new { id });
    }

}