using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;

namespace todolist.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly UserManager<AppUser> _users;
    private readonly SignInManager<AppUser> _signins;
    private readonly AppDbContext _db;

    public IndexModel(ILogger<IndexModel> logger, UserManager<AppUser> users, SignInManager<AppUser> signins, AppDbContext db)
    {
        _logger = logger;
        _users = users;
        _signins = signins;
        _db = db;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostDemoAsync()
    {
        string suffix = Guid.NewGuid().ToString("N")[..8];
        string email = $"demo_{suffix}@example.local";
        string userName = $"demo_{suffix}";

        AppUser user = new()
        {
            UserName = userName,
            Email = email,
            EmailConfirmed = true
        };
        string password = $"Demo!{Guid.NewGuid():N}aA1";
        IdentityResult createResult = await _users.CreateAsync(user, password);

        if (!createResult.Succeeded)
        {
            ModelState.AddModelError("", "Could not create demo user.");
            foreach (IdentityError err in createResult.Errors)
                _logger.LogWarning("Demo user creation error: {Code} {Desc}", err.Code, err.Description);
            return Page();
        }

        await _users.AddClaimAsync(user, new Claim("demo", "true"));

        await SeedDemoDataAsync(user);

        await _signins.SignInAsync(user, isPersistent: false);

        return RedirectToPage("/Index");
    }

    private async Task SeedDemoDataAsync(AppUser user)
    {
        string userId = user.Id;
        using IDbContextTransaction tx = await _db.Database.BeginTransactionAsync();

        AddDemoTodos(_db, userId);
        AddDemoChecklists(_db, userId);
        AddDemoGoals(_db, userId);
        AddDemoProjects(_db, userId);

        await _db.SaveChangesAsync();
        await tx.CommitAsync();
    }

    private static void AddDemoChecklists(AppDbContext db, string userId)
    {
        Checklist checklist = new() { Name = "My shopping list", UserId = userId };
        db.Checklists.Add(checklist);
        db.ChecklistTasks.AddRange(
            new ChecklistTask { Checklist = checklist, Description = "Chicken", Complete = false, Order = 1, UserId = userId },
            new ChecklistTask { Checklist = checklist, Description = "Rice", Complete = true, Order = 2, UserId = userId },
            new ChecklistTask { Checklist = checklist, Description = "Milk", Complete = false, Order = 3, UserId = userId },
            new ChecklistTask { Checklist = checklist, Description = "Raisin bran", Complete = true, Order = 4, UserId = userId },
            new ChecklistTask { Checklist = checklist, Description = "Eggs", Complete = false, Order = 5, UserId = userId }
        );
    }

    private static void AddDemoGoals(AppDbContext db, string userId)
    {
        Goal goal = new() { Name = "Lose weight", UserId = userId };
        Todo todo = new() { Description = "Buy a scale", UserId = userId, GoalId = goal.Id };
        ProgressNote note = new() { Text = "I gained some weight recently. I need to lose a bit of weight. Firstly I will start weighing myself more often so I can see if I am gaining or losing weight.", UserId = userId, GoalId = goal.Id };
        goal.Todos = [todo];
        goal.ProgressNotes = [note];
        db.Goals.Add(goal);
    }

    private static void AddDemoProjects(AppDbContext db, string userId)
    {
        Project project = new() { Name = "Nutrition app", UserId = userId };
        Todo todo = new() { Description = "Determine/plan scope", UserId = userId, ProjectId = project.Id };
        project.Todos = [todo];
        db.Projects.Add(project);
    }

    private static void AddDemoTodos(AppDbContext db, string userId)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        Todo CreateWorkShift(DateOnly day, int startHour24, int durationHours, bool completed)
        {
            var start = new TimeOnly(startHour24, 0);
            var end = start.AddHours(durationHours);

            var t = new Todo
            {
                Description = $"Work shift {day:MMM d} ({start:HH:mm}-{end:HH:mm})",
                Completed = completed,
                OnDate = day,
                StartTime = start,
                EndTime = end,
                UserId = userId
            };

            return t;
        }

        var todos = new List<Todo>
        {
            CreateWorkShift(today.AddDays(-3), 9, 8, completed: true),
            CreateWorkShift(today.AddDays(-2), 10, 8, completed: true),
            CreateWorkShift(today.AddDays(-1), 9, 7, completed: true),

            CreateWorkShift(today, 12, 6, completed: false),
            CreateWorkShift(today.AddDays(1), 9, 8, completed: false),
            CreateWorkShift(today.AddDays(2), 9, 8, completed: false),
            CreateWorkShift(today.AddDays(3), 10, 7, completed: false)
        };

        todos.AddRange
        ([
            new Todo
            {
                Description = "Review weekly goals",
                Completed = false,
                OnDate = today.AddDays(1),
                StartTime = new TimeOnly(8, 30),
                EndTime = new TimeOnly(9, 0),
                UserId = userId
            },
            new Todo
            {
                Description = "Buy groceries",
                Completed = false,
                UserId = userId
            },
            new Todo
            {
                Description = "Mark something done ✅",
                Completed = true,
                OnDate = today.AddDays(-2),
                StartTime = new TimeOnly(18, 0),
                EndTime = new TimeOnly(18, 15),
                UserId = userId
            },
        ]);

        db.Todos.AddRange(todos);
    }

}
