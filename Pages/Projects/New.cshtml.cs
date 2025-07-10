namespace todolist.Pages.Projects;

public class NewModel(AppDbContext db, UserManager<AppUser> users) : AppPageModel(db, users)
{
    [BindProperty]
    public Project Project { get; set; } = null!;

    public IActionResult OnGet()
    {
        string userId = UserId();

        Project = new() { Name = "", UserId = userId };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        if (Project.UserId != UserId()) return BadRequest();

        _db.Projects.Add(Project);
        await _db.SaveChangesAsync();

        return LocalRedirect("/Projects/Index");
    }
}