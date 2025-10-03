using MealMate_backend.Application.DTOs.MealGroupDishes;

namespace MealMate_backend.Application.Interfaces;

public interface IMealGroupDishService
{
    Task<IReadOnlyCollection<MealGroupDishDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MealGroupDishDto?> GetAsync(int mealGroupId, int dishId, CancellationToken cancellationToken = default);
    Task<MealGroupDishDto> CreateAsync(CreateMealGroupDishRequest request, CancellationToken cancellationToken = default);
    Task<MealGroupDishDto?> UpdateAsync(int mealGroupId, int dishId, UpdateMealGroupDishRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int mealGroupId, int dishId, CancellationToken cancellationToken = default);
}
