using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace todolist.Models;

public abstract class Entity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey(nameof(AppUser))]
    public string UserId { get; set; } = null!;
    [ValidateNever]
    public AppUser User { get; set; } = null!;
}