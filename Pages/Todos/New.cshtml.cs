using Microsoft.AspNetCore.Mvc.Rendering;

namespace todolist.Pages.Todos;

public class NewModel(AppDbContext db, UserManager<AppUser> users)
            : AppPageModel(db, users)
{
    [BindProperty]
    public TodoFormViewModel TodoForm { get; set; } = null!;

    public List<SelectListItem> AvailableGoals { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        string userId = UserId();

        List<Goal> goals = await _db.Goals
            .Where(g => g.UserId == userId).ToListAsync();

        AvailableGoals = [.. goals.Select(g =>
            new SelectListItem { Value=g.Id, Text=g.Name })];

        TodoForm = new() { Description = "", AvailableGoals=AvailableGoals };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        string userId = UserId();
        Todo todo = new()
        {
            UserId = userId,
            Description = TodoForm.Description,
            OnDate = TodoForm.OnDate
        };

        List<Goal> selectedGoals = await _db.Goals
            .Where(g => g.UserId == userId && TodoForm.SelectedGoalIds.Contains(g.Id))
            .ToListAsync();

        todo.Goals = selectedGoals;

        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();
        return LocalRedirect("/Todos/Index");
    }
}