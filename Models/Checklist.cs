namespace todolist.Models;

public class Checklist : Entity
{
    [Required]
    public string Name { get; set; } = null!;

    public List<ChecklistTask> Tasks { get; set; } = [];
}