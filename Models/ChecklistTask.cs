using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace todolist.Models;

public class ChecklistTask : Entity
{
    [Required]
    public string Description { get; set; } = null!;
    public bool Complete { get; set; }
    
    [ForeignKey(nameof(Checklist))]
    public string ChecklistId { get; set; } = null!;
    [ValidateNever]
    public Checklist Checklist { get; set; } = null!;
}