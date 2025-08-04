namespace todolist.Pages.Checklists;

public class PrintModel(AppDbContext db, UserManager<AppUser> users)
                            : AppPageModel(db, users)
{
    public Checklist Checklist { get; set; } = null!;

    public async Task<IActionResult> OnGet(string id)
    {
        string userId = UserId();

        Checklist? checklist = await _db.Checklists
            .Include(cl => cl.Tasks)
            .FirstOrDefaultAsync(c =>c.Id == id && c.UserId == userId);
        if (checklist == null) return NotFound();

        Checklist = checklist;

        return Page();
    }
}
