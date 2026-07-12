using MealMate_backend.Application.DTOs.Shopping;

namespace MealMate_backend.Application.Interfaces;

public interface IShoppingListService
{
    Task<IReadOnlyCollection<ShoppingListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ShoppingListItemDto>> SaveAsync(SaveShoppingListRequest request, CancellationToken cancellationToken = default);
    Task<ShoppingListItemDto> CreateItemAsync(CreateShoppingListItemRequest request, CancellationToken cancellationToken = default);
    Task<ShoppingListItemDto?> UpdateItemAsync(int id, UpdateShoppingListItemRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteItemAsync(int id, CancellationToken cancellationToken = default);
    Task ClearAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ShoppingListItemDto>> AddBaseProductsAsync(CancellationToken cancellationToken = default);
}
