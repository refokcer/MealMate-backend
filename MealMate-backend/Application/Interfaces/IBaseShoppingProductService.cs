using MealMate_backend.Application.DTOs.Shopping;

namespace MealMate_backend.Application.Interfaces;

public interface IBaseShoppingProductService
{
    Task<IReadOnlyCollection<BaseShoppingProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BaseShoppingProductDto> CreateAsync(CreateBaseShoppingProductRequest request, CancellationToken cancellationToken = default);
    Task<BaseShoppingProductDto?> UpdateAsync(int id, UpdateBaseShoppingProductRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
