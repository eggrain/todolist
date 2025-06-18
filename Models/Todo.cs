using System.ComponentModel.DataAnnotations.Schema;

namespace todolist.Models;

public class Todo : Entity
{
    public string Description { get; set; } = null!;

    public List<Goal> Goals { get; set; } = [];

    [ForeignKey(nameof(Project))]
    public string? ProjectId { get; set; }

    public Project? Project { get; set; }

    public DateOnly? OnDate { get; set; }
}