namespace todolist.Pages.Todos;

public class IndexModel(AppDbContext db, UserManager<AppUser> users)
                                        : AppPageModel(db, users)
{
    public List<Todo> Todos { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        string userId = UserId();

        Todos = await _db.Todos.Where(t => t.UserId == userId).ToListAsync();

        return Page();
    }                                    
}