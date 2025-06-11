using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace todolist.Pages.Identity;

public class LogoutModel(SignInManager<IdentityUser> signInManager) : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

    public async Task<IActionResult> OnPostAsync()
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect("/");
    }
}

