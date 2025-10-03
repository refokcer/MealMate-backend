namespace MealMate_backend.Application.DTOs.Dishes;

public record DishDto(
    int Id,
    string Name,
    string? Description,
    string? Instructions,
    int? PreparationMinutes,
    string? ImageUrl,
    IReadOnlyCollection<DishProductSummaryDto> Products,
    IReadOnlyCollection<MealGroupSummaryDto> MealGroups);

public record DishProductSummaryDto(int ProductId, string ProductName, string? Quantity);

public record MealGroupSummaryDto(int MealGroupId, string MealGroupName);
