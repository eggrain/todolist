namespace todolist.Models;

public class Todo : Entity
{
    public string Description { get; set; } = null!;
    public bool Completed { get; set; } = false;

    [ForeignKey(nameof(Project))]
    public string? ProjectId { get; set; }
    public Project? Project { get; set; }

    public DateOnly? OnDate { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }

    [ForeignKey(nameof(Goal))]
    public string? GoalId { get; set; }
    public Goal? Goal { get; set; }
}
