namespace todolist.Models;

public class Goal(string name) : Entity
{
    public string Name { get; set; } = name;

    public List<Todo> Todos { get; set; } = [];
}
