namespace todolist.Pages.Projects;

public class DeleteModel(AppDbContext db, UserManager<AppUser> users)
                                            : AppPageModel(db, users)
{
    public async Task<IActionResult> OnPostAsync(string id)
    {
        string userId = UserId();

        Project? proj = await _db.Projects.Include(p => p.Todos)
                        .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (proj == null) return NotFound();

        foreach (Todo todo in proj.Todos) todo.ProjectId = null;
        _db.Projects.Remove(proj);

        await _db.SaveChangesAsync();

        return RedirectToPage("/Projects/Index");
    }
}