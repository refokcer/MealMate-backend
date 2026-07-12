namespace MealMate_backend.Application.DTOs.Shopping;

public class UpdateBaseShoppingProductRequest
{
    public int ProductId { get; set; }
    public decimal? Quantity { get; set; }
    public string? Notes { get; set; }
}
