using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace todolist.Pages;

[Authorize]
public abstract class AppPageModel(AppDbContext db,
                                UserManager<AppUser> users) : PageModel
{
    protected readonly AppDbContext _db = db;
    protected readonly UserManager<AppUser> _users = users;

    protected string UserId()
    {
        // This will never be null because of [Authorize]
        return _users.GetUserId(User)!;
    }
}