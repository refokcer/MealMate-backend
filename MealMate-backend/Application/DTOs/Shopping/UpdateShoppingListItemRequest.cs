namespace MealMate_backend.Application.DTOs.Shopping;

public class UpdateShoppingListItemRequest
{
    public decimal? Quantity { get; set; }
    public bool IsChecked { get; set; }
    public string? Notes { get; set; }
}
