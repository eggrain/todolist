using System.ComponentModel.DataAnnotations.Schema;

namespace todolist.Models.ShoppingLists;

public class StoreItem : Entity
{
    [Required]
    public string Name { get; set; } = null!;

    public Store Store { get; set; } = null!;

    [ForeignKey(nameof(Store))]
    public string StoreId { get; set; } = null!;
}
