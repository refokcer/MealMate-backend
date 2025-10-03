using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Application.DTOs.MealGroupDishes;

public class CreateMealGroupDishRequest
{
    [Required]
    public int MealGroupId { get; set; }

    [Required]
    public int DishId { get; set; }
}
