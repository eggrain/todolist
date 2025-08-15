namespace todolist.Pages.Checklists;

public class DeleteModel(AppDbContext db, UserManager<AppUser> users)
                                        : AppPageModel(db, users)
{
    public async Task<IActionResult> OnPostAsync(string id)
    {
        string userId = UserId();

        Checklist? checklist = await _db.Checklists.FirstOrDefaultAsync(
            cl => cl.UserId == userId && cl.Id == id
        );

        if (checklist == null) return NotFound();

        _db.Checklists.Remove(checklist);
        await _db.SaveChangesAsync();

        return RedirectToPage("/Checklists/Index");
    }
}
