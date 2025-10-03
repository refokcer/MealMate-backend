using MealMate_backend.Application.DTOs.Dishes;

namespace MealMate_backend.Application.Interfaces;

public interface IDishService
{
    Task<IReadOnlyCollection<DishDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DishDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DishDto> CreateAsync(CreateDishRequest request, CancellationToken cancellationToken = default);
    Task<DishDto?> UpdateAsync(int id, UpdateDishRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
