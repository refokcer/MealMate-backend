namespace MealMate_backend.Application.DTOs.DishProducts;

public record DishProductDto(
    int DishId,
    string DishName,
    int ProductId,
    string ProductName,
    decimal? Quantity,
    string Unit);
