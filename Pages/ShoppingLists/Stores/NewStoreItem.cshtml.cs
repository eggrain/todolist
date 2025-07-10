using todolist.Models.ShoppingLists;

namespace todolist.Pages.ShoppingLists.Stores;

public class NewStoreItem(AppDbContext db, UserManager<AppUser> users)
                                        : AppPageModel(db, users)
{
    public Store Store { get; set; } = null!;
    [BindProperty] public StoreItem Item { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(string storeId)
    {
        string userId = UserId();

        Store? store = await _db.Stores.FirstOrDefaultAsync(
            s => s.UserId == userId && s.Id == storeId);

        if (store == null) return BadRequest();

        Store = store;
        Item = new() { UserId = userId, StoreId = Store.Id };

        return Page();
    }
}
