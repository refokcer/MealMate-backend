namespace MealMate_backend.Application.DTOs.Shopping;

public record BaseShoppingProductDto(
    int Id,
    int ProductId,
    string ProductName,
    string Unit,
    string? Category,
    decimal? Quantity,
    string? Notes);
