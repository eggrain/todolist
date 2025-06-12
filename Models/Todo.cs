namespace todolist.Models;

public class Todo(string description) : Entity
{
    public string Description { get; set; } = description;

    public List<Goal> Goals { get; set; } = [];

    public Project? Project { get; set;  }
}