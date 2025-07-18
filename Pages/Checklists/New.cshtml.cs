namespace todolist.Pages.Checklists;

public class NewModel(AppDbContext db, UserManager<AppUser> users)
                                            : AppPageModel(db, users)
{
    [BindProperty]
    public Checklist Checklist { get; set; } = null!;

    public IActionResult OnGet()
    {
        string userId = _users.GetUserId(User)!;

        Checklist = new() { UserId = userId };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        if (Checklist.UserId != UserId())
            return BadRequest();

        _db.Checklists.Add(Checklist);
        await _db.SaveChangesAsync();

        return LocalRedirect("/Checklists/Index");
    }
}