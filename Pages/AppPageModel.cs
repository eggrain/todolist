using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todolist.Data;
using todolist.Models;

namespace todolist.Pages;

[Authorize]
public abstract class AppPageModel(AppDbContext db,
                                UserManager<AppUser> users) : PageModel
{
    protected readonly AppDbContext _db = db;
    protected readonly UserManager<AppUser> _users = users;
}