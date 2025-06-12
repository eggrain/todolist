using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todolist.Models;

namespace todolist.Pages.Identity;

public class LogoutModel(SignInManager<AppUser> signInManager) : PageModel
{
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public async Task<IActionResult> OnPostAsync()
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect("/");
    }
}

