using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolist.Data;
using todolist.Models;

namespace todolist.Pages.Projects;

public class IndexModel(AppDbContext db,
                UserManager<AppUser> users) : AppPageModel(db, users)
{
    [BindProperty]
    public List<Project> Projects { get; private set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        string userId = UserId();

        Projects = await _db.Projects.AsNoTracking()
                    .Where(p => p.UserId == userId).ToListAsync();

        return Page();
    }
}