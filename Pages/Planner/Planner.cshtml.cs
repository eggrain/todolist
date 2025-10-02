namespace todolist.Pages.Planner;

public class PlannerModel
    (AppDbContext db, UserManager<AppUser> users)
    : AppPageModel(db, users)
{
    public int WeekOffset { get; private set; }
    public List<DateOnly> DaysOfWeek { get; private set; } = [];
    private readonly DateTime today = DateTime.Today;
    public string MonthYear { get; private set; } = null!;
    public Dictionary<DateOnly, List<Todo>> MapDayTodos { get; private set; } = [];

    public async Task<IActionResult> OnGetAsync(int weekOffset = 0)
    {
        string userId = UserId();
        WeekOffset = weekOffset;

        int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        DateTime baseMonday = today.AddDays(-diff);
        DateTime monday = baseMonday.AddDays(weekOffset * 7);

        DaysOfWeek = [.. Enumerable
            .Range(0, 7)
            .Select(i => DateOnly.FromDateTime(monday.AddDays(i)))];

        MonthYear = monday.ToString("MMMM yyyy");

        DateOnly start = DaysOfWeek.First();
        DateOnly end = DaysOfWeek.Last().AddDays(1);
        List<Todo> todosThisWeek = await _db.Todos
            .Where(t => t.OnDate >= start && t.OnDate < end && t.UserId == userId)
            .Include(t => t.Goal).Include(t => t.Project)
            .AsNoTracking()
            .ToListAsync();

        MapDayTodos = DaysOfWeek.ToDictionary(
            day => day,
            day => todosThisWeek
                       .Where(t => t.OnDate == day)
                       .ToList()
        );

        return Page();
    }

    public PlannerDayCardModel BuildPlannerDayViewModel(DateOnly day) => new()
    {
        Date = day,
        Todos = MapDayTodos.TryGetValue(day, out var list) ? list : [],
        UserId = UserId()
    };
}
