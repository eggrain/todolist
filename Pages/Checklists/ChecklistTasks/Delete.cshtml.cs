namespace todolist.Pages.Checklists.ChecklistTasks;

public class DeleteModel(AppDbContext db, UserManager<AppUser> users)
                                            : AppPageModel(db, users)
{
    [BindProperty]
    public string? ReturnUrl { get; set;  }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        string userId = UserId();

        ChecklistTask? task = await _db.ChecklistTasks
            .FirstOrDefaultAsync(cl => cl.Id == id && cl.UserId == userId);
        if (task == null) return NotFound();

        _db.ChecklistTasks.Remove(task);
        await _db.SaveChangesAsync();

        if (ReturnUrl != null)
            return LocalRedirect(ReturnUrl);

        return LocalRedirect("/Checklists/Index");
    }
}
