namespace todolist.Models;

public class Project(string name) : Entity
{
    public string Name { get; set; } = name;

    public List<Todo> Todos { get; set; } = [];
}