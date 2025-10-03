namespace MealMate_backend.Application.DTOs.MealGroupDishes;

public record MealGroupDishDto(
    int MealGroupId,
    string MealGroupName,
    int DishId,
    string DishName);
