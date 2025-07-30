namespace todolist.Models;

public class Goal : Entity
{
    [Required, MinLength(3)]
    public string Name { get; set; } = null!;

    public List<ProgressNote> ProgressNotes { get; set; } = [];

    public List<Todo> Todos { get; set; } = [];
}
