using Microsoft.AspNetCore.Mvc.Rendering;

namespace todolist.Pages.Todos;

public class NewModel(AppDbContext db, UserManager<AppUser> users)
            : AppPageModel(db, users)
{
    [BindProperty]
    public Todo Todo { get; set; } = null!;

    public List<SelectListItem> AvailableGoals { get; set; } = [];

    public IActionResult OnGet()
    {
        string userId = UserId();

        Todo = new() { Description = "", UserId = userId };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        string userId = UserId();
        Todo todo = new()
        {
            UserId = userId,
            Description = Todo.Description,
            OnDate = Todo.OnDate
        };

        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();
        return LocalRedirect("/Todos/Index");
    }
}