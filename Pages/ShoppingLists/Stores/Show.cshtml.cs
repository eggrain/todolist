using todolist.Models.ShoppingLists;

namespace todolist.Pages.ShoppingLists.Stores;

public class ShowModel(AppDbContext db, UserManager<AppUser> users)
                                        : AppPageModel(db, users)
{
    public Store Store { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync()
    {
        string userId = UserId();

        Store? store = await _db.Stores
            .FirstOrDefaultAsync(s => s.UserId == userId);
        if (store == null) return NotFound();

        Store = store;
        return Page();
    }
}
