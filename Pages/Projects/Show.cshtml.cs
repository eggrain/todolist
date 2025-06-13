namespace todolist.Pages.Projects;

public class ShowModel(AppDbContext db, UserManager<AppUser> users)
                                    : AppPageModel(db, users)
{
    public Project Project { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = UserId();

        Project? project = await _db.Projects
                .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == id);
        if (project == null) return NotFound();

        Project = project;
        return Page();
    }
}