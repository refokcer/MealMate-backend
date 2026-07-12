using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Application.DTOs.DishProducts;

public class CreateDishProductRequest
{
    [Required]
    public int DishId { get; set; }

    [Required]
    public int ProductId { get; set; }

    public decimal? Quantity { get; set; }
}
