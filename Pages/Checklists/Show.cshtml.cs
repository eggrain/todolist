namespace todolist.Pages.Checklists;

public class ShowModel(AppDbContext db, UserManager<AppUser> users)
                    : AppPageModel(db, users)
{
    public Checklist Checklist { get; private set; } = null!;
    public ChecklistTask NewTask { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = _users.GetUserId(User)!;

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
}