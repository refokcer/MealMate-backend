using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Application.DTOs.DishProducts;

public class UpdateDishProductRequest
{
    [MaxLength(80)]
    public string? Quantity { get; set; }
}
