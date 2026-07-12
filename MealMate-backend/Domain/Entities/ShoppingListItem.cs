using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Domain.Entities;

public class ShoppingListItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public decimal? Quantity { get; set; }

    public bool IsChecked { get; set; }

    [MaxLength(200)]
    public string? Notes { get; set; }
}
