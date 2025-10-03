using MealMate_backend.Application.DTOs.DishProducts;

namespace MealMate_backend.Application.Interfaces;

public interface IDishProductService
{
    Task<IReadOnlyCollection<DishProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DishProductDto?> GetAsync(int dishId, int productId, CancellationToken cancellationToken = default);
    Task<DishProductDto> CreateAsync(CreateDishProductRequest request, CancellationToken cancellationToken = default);
    Task<DishProductDto?> UpdateAsync(int dishId, int productId, UpdateDishProductRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int dishId, int productId, CancellationToken cancellationToken = default);
}
