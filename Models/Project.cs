namespace todolist.Models;

public class Project : Entity
{
    [Required]
    public string Name { get; set; } = null!;

    public List<Todo> Todos { get; set; } = [];

    [Required]
    public string PurposeStatement { get; set; } = "";
}
