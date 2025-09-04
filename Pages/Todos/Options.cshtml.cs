namespace todolist.Pages.Todos;

public class OptionsModel(AppDbContext db, UserManager<AppUser> users) : AppPageModel(db, users)
{
    public async Task<IActionResult> OnGetAsync()
    {
        string userId = UserId();

        var projects = await _db.Projects
            .Where(p => p.UserId == userId)
            .OrderBy(p => p.Name)
            .Select(p => new { id = p.Id, name = p.Name })
            .ToListAsync();

        var goals = await _db.Goals
            .Where(g => g.UserId == userId)
            .OrderBy(g => g.Name)
            .Select(g => new { id = g.Id, name = g.Name })
            .ToListAsync();

        return new JsonResult(new { projects, goals });
    }
}
