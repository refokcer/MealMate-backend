namespace MealMate_backend.Application.DTOs.Shopping;

public record ShoppingListItemDto(
    int Id,
    int ProductId,
    string ProductName,
    string Unit,
    string? Category,
    decimal? Quantity,
    bool IsChecked,
    string? Notes);
