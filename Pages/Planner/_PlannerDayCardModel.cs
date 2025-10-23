namespace todolist.Pages.Planner;

public class PlannerDayCardModel
{
    public DateOnly Date { get; set; }
    public List<Todo> Todos { get; set; } = [];
    public required string UserId { get; set; }
    public TimeSpan TimeSum { get; set;  }
}
