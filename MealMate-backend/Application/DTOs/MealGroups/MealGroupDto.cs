namespace MealMate_backend.Application.DTOs.MealGroups;

public record MealGroupDto(
    int Id,
    string Name,
    string? Description,
    string? AccentColor,
    IReadOnlyCollection<MealGroupDishSummaryDto> Dishes);

public record MealGroupDishSummaryDto(int DishId, string DishName);
