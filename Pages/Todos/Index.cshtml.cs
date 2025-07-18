namespace todolist.Pages.Todos;

public class IndexModel(AppDbContext db, UserManager<AppUser> users)
                                        : AppPageModel(db, users)
{
    public List<Todo> Todos { get; set; } = [];
    public List<Todo> Completed { get; set; } = [];
    public List<Todo> Incompleted { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        string userId = UserId();

        Todos = await _db.Todos.Include(t => t.Project).Include(t => t.Goal)
                        .Where(t => t.UserId == userId)
                        .OrderBy(t => t.OnDate == null)
                        .ThenBy(t => t.OnDate).ToListAsync();

        Completed = [.. Todos.Where(t => t.Completed == true)];
        Incompleted = [.. Todos.Where(t => t.Completed != true)];

        return Page();
    }                                    
}