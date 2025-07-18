namespace todolist.Pages.Checklists;

public class IndexModel(AppDbContext db, UserManager<AppUser> users)
                         : AppPageModel(db, users)
{
    public List<Checklist> Checklists { get; private set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        string userId = UserId();

        Checklists = await _db.Checklists
            .Where(cl => cl.UserId == userId).ToListAsync();

        return Page();
    }
}