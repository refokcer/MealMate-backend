using MealMate_backend.Application.DTOs.Dishes;
using MealMate_backend.Application.Interfaces;
using MealMate_backend.Domain.Entities;
using MealMate_backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate_backend.Application.Services;

public class DishService : IDishService
{
    private readonly MealMateDbContext _context;

    public DishService(MealMateDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<DishDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var dishes = await _context.Dishes
            .AsNoTracking()
            .Include(d => d.DishProducts)
                .ThenInclude(dp => dp.Product)
            .Include(d => d.MealGroupDishes)
                .ThenInclude(mgd => mgd.MealGroup)
            .ToListAsync(cancellationToken);

        return dishes.Select(MapToDto).ToList();
    }

    public async Task<DishDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var dish = await _context.Dishes
            .AsNoTracking()
            .Include(d => d.DishProducts)
                .ThenInclude(dp => dp.Product)
            .Include(d => d.MealGroupDishes)
                .ThenInclude(mgd => mgd.MealGroup)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        return dish is null ? null : MapToDto(dish);
    }

    public async Task<DishDto> CreateAsync(CreateDishRequest request, CancellationToken cancellationToken = default)
    {
        var dish = new Dish
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim(),
            Instructions = request.Instructions?.Trim(),
            PreparationMinutes = request.PreparationMinutes,
            ImageUrl = request.ImageUrl?.Trim()
        };

        await _context.Dishes.AddAsync(dish, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(dish.Id, cancellationToken))!;
    }

    public async Task<DishDto?> UpdateAsync(int id, UpdateDishRequest request, CancellationToken cancellationToken = default)
    {
        var dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (dish is null)
        {
            return null;
        }

        dish.Name = request.Name.Trim();
        dish.Description = request.Description?.Trim();
        dish.Instructions = request.Instructions?.Trim();
        dish.PreparationMinutes = request.PreparationMinutes;
        dish.ImageUrl = request.ImageUrl?.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(dish.Id, cancellationToken))!;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (dish is null)
        {
            return false;
        }

        _context.Dishes.Remove(dish);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static DishDto MapToDto(Dish dish)
    {
        var products = dish.DishProducts
            .Select(dp => new DishProductSummaryDto(
                dp.ProductId,
                dp.Product.Name,
                dp.Quantity))
            .ToList();

        var mealGroups = dish.MealGroupDishes
            .Select(mgd => new MealGroupSummaryDto(
                mgd.MealGroupId,
                mgd.MealGroup.Name))
            .ToList();

        return new DishDto(
            dish.Id,
            dish.Name,
            dish.Description,
            dish.Instructions,
            dish.PreparationMinutes,
            dish.ImageUrl,
            products,
            mealGroups);
    }
}
