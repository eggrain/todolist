using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace todolist.Models;

public class ProgressNote : Entity
{
    [ForeignKey(nameof(Goal))]
    public string GoalId { get; set; } = null!;
    [ValidateNever]
    public Goal Goal { get; set; } = null!;
    [Required]
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
