using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(40)]
    public string? Category { get; set; }

    [Required]
    [MaxLength(20)]
    public string Unit { get; set; } = "шт.";

    [MaxLength(200)]
    public string? Notes { get; set; }

    public ICollection<DishProduct> DishProducts { get; set; } = new List<DishProduct>();
    public ICollection<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();
    public ICollection<BaseShoppingProduct> BaseShoppingProducts { get; set; } = new List<BaseShoppingProduct>();
}
