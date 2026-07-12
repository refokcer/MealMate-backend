namespace MealMate_backend.Application.DTOs.Shopping;

public class SaveShoppingListRequest
{
    public IReadOnlyCollection<SaveShoppingListItemRequest> Items { get; set; } = Array.Empty<SaveShoppingListItemRequest>();
}

public class SaveShoppingListItemRequest
{
    public int ProductId { get; set; }
    public decimal? Quantity { get; set; }
    public bool IsChecked { get; set; }
    public string? Notes { get; set; }
}
