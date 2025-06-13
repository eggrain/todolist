namespace todolist.Models;

public class Project : Entity
{
    public string Name { get; set; } = null!;

    public List<Todo> Todos { get; set; } = [];
}