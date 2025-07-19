namespace todolist.Pages.Projects;

public class EditModel(AppDbContext db, UserManager<AppUser> users)
                                            : AppPageModel(db, users)
{
    [BindProperty]
    public Project Project { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = UserId();

        Project? project = await _db.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return NotFound();

        Project = project;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (!ModelState.IsValid) return Page();
        string userId = UserId();

        Project? project = await _db.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return NotFound();

        project.Name = Project.Name;
        await _db.SaveChangesAsync();

        return LocalRedirect($"/Projects/Show/{Project.Id}");
    }
}