using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Domain.Entities;

public class BaseShoppingProduct
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public decimal? Quantity { get; set; }

    [MaxLength(200)]
    public string? Notes { get; set; }
}
