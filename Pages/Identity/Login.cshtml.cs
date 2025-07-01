using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace todolist.Pages.Identity;

public class LoginModel(SignInManager<AppUser> signInManager) : PageModel
{
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    [BindProperty]
    public InputModel Input { get; set; } = null!;

    public class InputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        Microsoft.AspNetCore.Identity.SignInResult res =
             await _signInManager.PasswordSignInAsync(
          Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

        if (res.Succeeded) return LocalRedirect("/");

        ModelState.AddModelError("", "Invalid login attempt.");
        return Page();
    }
}