namespace todolist.Models.ShoppingLists;

public class Store : Entity
{
    [Required]
    public string Name { get; set; } = null!;
}
