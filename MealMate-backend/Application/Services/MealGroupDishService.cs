using MealMate_backend.Application.DTOs.MealGroupDishes;
using MealMate_backend.Application.Interfaces;
using MealMate_backend.Domain.Entities;
using MealMate_backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate_backend.Application.Services;

public class MealGroupDishService : IMealGroupDishService
{
    private readonly MealMateDbContext _context;

    public MealGroupDishService(MealMateDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<MealGroupDishDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var links = await _context.MealGroupDishes
            .AsNoTracking()
            .Include(mgd => mgd.MealGroup)
            .Include(mgd => mgd.Dish)
            .ToListAsync(cancellationToken);

        return links.Select(MapToDto).ToList();
    }

    public async Task<MealGroupDishDto?> GetAsync(int mealGroupId, int dishId, CancellationToken cancellationToken = default)
    {
        var link = await _context.MealGroupDishes
            .AsNoTracking()
            .Include(mgd => mgd.MealGroup)
            .Include(mgd => mgd.Dish)
            .FirstOrDefaultAsync(mgd => mgd.MealGroupId == mealGroupId && mgd.DishId == dishId, cancellationToken);

        return link is null ? null : MapToDto(link);
    }

    public async Task<MealGroupDishDto> CreateAsync(CreateMealGroupDishRequest request, CancellationToken cancellationToken = default)
    {
        await EnsureMealGroupExists(request.MealGroupId, cancellationToken);
        await EnsureDishExists(request.DishId, cancellationToken);

        var exists = await _context.MealGroupDishes
            .AnyAsync(mgd => mgd.MealGroupId == request.MealGroupId && mgd.DishId == request.DishId, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Связь между набором и блюдом уже существует.");
        }

        var link = new MealGroupDish
        {
            MealGroupId = request.MealGroupId,
            DishId = request.DishId
        };

        await _context.MealGroupDishes.AddAsync(link, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return (await GetAsync(link.MealGroupId, link.DishId, cancellationToken))!;
    }

    public async Task<MealGroupDishDto?> UpdateAsync(int mealGroupId, int dishId, UpdateMealGroupDishRequest request, CancellationToken cancellationToken = default)
    {
        var link = await _context.MealGroupDishes
            .FirstOrDefaultAsync(mgd => mgd.MealGroupId == mealGroupId && mgd.DishId == dishId, cancellationToken);
        if (link is null)
        {
            return null;
        }

        if (mealGroupId == request.MealGroupId && dishId == request.DishId)
        {
            return (await GetAsync(mealGroupId, dishId, cancellationToken))!;
        }

        await EnsureMealGroupExists(request.MealGroupId, cancellationToken);
        await EnsureDishExists(request.DishId, cancellationToken);

        var exists = await _context.MealGroupDishes
            .AnyAsync(mgd => mgd.MealGroupId == request.MealGroupId && mgd.DishId == request.DishId, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Такая связь уже существует.");
        }

        _context.MealGroupDishes.Remove(link);
        await _context.MealGroupDishes.AddAsync(new MealGroupDish
        {
            MealGroupId = request.MealGroupId,
            DishId = request.DishId
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return (await GetAsync(request.MealGroupId, request.DishId, cancellationToken))!;
    }

    public async Task<bool> DeleteAsync(int mealGroupId, int dishId, CancellationToken cancellationToken = default)
    {
        var link = await _context.MealGroupDishes
            .FirstOrDefaultAsync(mgd => mgd.MealGroupId == mealGroupId && mgd.DishId == dishId, cancellationToken);
        if (link is null)
        {
            return false;
        }

        _context.MealGroupDishes.Remove(link);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task EnsureMealGroupExists(int mealGroupId, CancellationToken cancellationToken)
    {
        var exists = await _context.MealGroups.AnyAsync(mg => mg.Id == mealGroupId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Набор с идентификатором {mealGroupId} не найден.");
        }
    }

    private async Task EnsureDishExists(int dishId, CancellationToken cancellationToken)
    {
        var exists = await _context.Dishes.AnyAsync(d => d.Id == dishId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Блюдо с идентификатором {dishId} не найдено.");
        }
    }

    private static MealGroupDishDto MapToDto(MealGroupDish link)
        => new(
            link.MealGroupId,
            link.MealGroup.Name,
            link.DishId,
            link.Dish.Name);
}
