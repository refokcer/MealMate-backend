using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Domain.Entities;

public class DishProduct
{
    public int DishId { get; set; }
    public Dish Dish { get; set; } = default!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public decimal? Quantity { get; set; }
}
