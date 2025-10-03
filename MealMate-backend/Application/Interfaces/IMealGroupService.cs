using MealMate_backend.Application.DTOs.MealGroups;

namespace MealMate_backend.Application.Interfaces;

public interface IMealGroupService
{
    Task<IReadOnlyCollection<MealGroupDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MealGroupDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<MealGroupDto> CreateAsync(CreateMealGroupRequest request, CancellationToken cancellationToken = default);
    Task<MealGroupDto?> UpdateAsync(int id, UpdateMealGroupRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
