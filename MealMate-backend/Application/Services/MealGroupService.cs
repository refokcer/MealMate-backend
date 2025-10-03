using MealMate_backend.Application.DTOs.MealGroups;
using MealMate_backend.Application.Interfaces;
using MealMate_backend.Domain.Entities;
using MealMate_backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate_backend.Application.Services;

public class MealGroupService : IMealGroupService
{
    private readonly MealMateDbContext _context;

    public MealGroupService(MealMateDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<MealGroupDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var groups = await _context.MealGroups
            .AsNoTracking()
            .Include(mg => mg.MealGroupDishes)
                .ThenInclude(mgd => mgd.Dish)
            .ToListAsync(cancellationToken);

        return groups.Select(MapToDto).ToList();
    }

    public async Task<MealGroupDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var group = await _context.MealGroups
            .AsNoTracking()
            .Include(mg => mg.MealGroupDishes)
                .ThenInclude(mgd => mgd.Dish)
            .FirstOrDefaultAsync(mg => mg.Id == id, cancellationToken);

        return group is null ? null : MapToDto(group);
    }

    public async Task<MealGroupDto> CreateAsync(CreateMealGroupRequest request, CancellationToken cancellationToken = default)
    {
        var group = new MealGroup
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim(),
            AccentColor = request.AccentColor?.Trim()
        };

        await _context.MealGroups.AddAsync(group, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(group.Id, cancellationToken))!;
    }

    public async Task<MealGroupDto?> UpdateAsync(int id, UpdateMealGroupRequest request, CancellationToken cancellationToken = default)
    {
        var group = await _context.MealGroups.FirstOrDefaultAsync(mg => mg.Id == id, cancellationToken);
        if (group is null)
        {
            return null;
        }

        group.Name = request.Name.Trim();
        group.Description = request.Description?.Trim();
        group.AccentColor = request.AccentColor?.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(group.Id, cancellationToken))!;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var group = await _context.MealGroups.FirstOrDefaultAsync(mg => mg.Id == id, cancellationToken);
        if (group is null)
        {
            return false;
        }

        _context.MealGroups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static MealGroupDto MapToDto(MealGroup group)
    {
        var dishes = group.MealGroupDishes
            .Select(mgd => new MealGroupDishSummaryDto(
                mgd.DishId,
                mgd.Dish.Name))
            .ToList();

        return new MealGroupDto(
            group.Id,
            group.Name,
            group.Description,
            group.AccentColor,
            dishes);
    }
}
