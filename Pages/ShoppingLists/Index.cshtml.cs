using todolist.Models.ShoppingLists;

namespace todolist.Pages.ShoppingLists;

public class IndexModel(AppDbContext db, UserManager<AppUser> users)
                                    : AppPageModel(db, users)
{
    public List<Store> Stores { get; private set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        string userId = UserId();

        Stores = await _db.Stores.Where(s => s.UserId == userId)
                    .ToListAsync();

        return Page();
    }
}