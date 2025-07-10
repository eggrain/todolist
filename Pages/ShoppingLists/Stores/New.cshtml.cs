using todolist.Models.ShoppingLists;

namespace todolist.Pages.ShoppingLists.Stores;

public class NewModel(AppDbContext db, UserManager<AppUser> users)
                        : AppPageModel(db, users)
{
    [BindProperty] public Store Store { get; set; } = null!;

    public IActionResult OnGet()
    {
        string userId = UserId();

        Store = new() { UserId = userId };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        if (Store.UserId != UserId()) return BadRequest();

        _db.Stores.Add(Store);
        await _db.SaveChangesAsync();

        return LocalRedirect("/ShoppingLists/Index");
    }
}