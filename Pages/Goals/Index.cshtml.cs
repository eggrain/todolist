using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using todolist.Data;
using todolist.Models;

namespace todolist.Pages.Goals;

public class IndexModel(AppDbContext db, UserManager<AppUser> users)
                                        : AppPageModel(db, users)
{
    public List<Goal> Goals { get; private set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        string userId = _users.GetUserId(User)!;

        Goals = await _db.Goals.Where(g => g.UserId == userId).ToListAsync();

        return Page();
    }
}