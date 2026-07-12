namespace MealMate_backend.Application.DTOs.Shopping;

public class CreateShoppingListItemRequest
{
    public int ProductId { get; set; }
    public decimal? Quantity { get; set; }
    public string? Notes { get; set; }
}
