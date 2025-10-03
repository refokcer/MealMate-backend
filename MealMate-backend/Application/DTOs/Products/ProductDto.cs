namespace MealMate_backend.Application.DTOs.Products;

public record ProductDto(
    int Id,
    string Name,
    string? Category,
    string? Notes,
    IReadOnlyCollection<ProductDishSummaryDto> Dishes);

public record ProductDishSummaryDto(int DishId, string DishName, string? Quantity);
