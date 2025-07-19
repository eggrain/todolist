namespace todolist.Pages.Checklists;

public class EditModel(AppDbContext db, UserManager<AppUser> users)
                                : AppPageModel(db, users)
{
    [BindProperty] public Checklist Checklist { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        string userId = UserId();

        Checklist? checklist = await _db.Checklists
            .FirstOrDefaultAsync(cl => cl.Id == id && cl.UserId == userId);
        if (checklist == null) return NotFound();
        Checklist = checklist;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (!ModelState.IsValid) return Page();
        string userId = UserId();

        Checklist? checklist = await _db.Checklists
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (checklist == null) return NotFound();

        checklist.Name = Checklist.Name;
        await _db.SaveChangesAsync();

        return LocalRedirect($"/Checklists/Show/{id}");
    }
}
