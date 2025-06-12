using System.ComponentModel.DataAnnotations;

namespace todolist.Models;

public abstract class Entity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public AppUser User { get; set; } = null!;
}