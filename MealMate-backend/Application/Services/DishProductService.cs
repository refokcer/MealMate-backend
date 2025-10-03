using MealMate_backend.Application.DTOs.DishProducts;
using MealMate_backend.Application.Interfaces;
using MealMate_backend.Domain.Entities;
using MealMate_backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate_backend.Application.Services;

public class DishProductService : IDishProductService
{
    private readonly MealMateDbContext _context;

    public DishProductService(MealMateDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<DishProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var links = await _context.DishProducts
            .AsNoTracking()
            .Include(dp => dp.Dish)
            .Include(dp => dp.Product)
            .ToListAsync(cancellationToken);

        return links.Select(MapToDto).ToList();
    }

    public async Task<DishProductDto?> GetAsync(int dishId, int productId, CancellationToken cancellationToken = default)
    {
        var link = await _context.DishProducts
            .AsNoTracking()
            .Include(dp => dp.Dish)
            .Include(dp => dp.Product)
            .FirstOrDefaultAsync(dp => dp.DishId == dishId && dp.ProductId == productId, cancellationToken);

        return link is null ? null : MapToDto(link);
    }

    public async Task<DishProductDto> CreateAsync(CreateDishProductRequest request, CancellationToken cancellationToken = default)
    {
        await EnsureDishExists(request.DishId, cancellationToken);
        await EnsureProductExists(request.ProductId, cancellationToken);

        var exists = await _context.DishProducts
            .AnyAsync(dp => dp.DishId == request.DishId && dp.ProductId == request.ProductId, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Связь между блюдом и продуктом уже существует.");
        }

        var link = new DishProduct
        {
            DishId = request.DishId,
            ProductId = request.ProductId,
            Quantity = request.Quantity?.Trim()
        };

        await _context.DishProducts.AddAsync(link, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return (await GetAsync(link.DishId, link.ProductId, cancellationToken))!;
    }

    public async Task<DishProductDto?> UpdateAsync(int dishId, int productId, UpdateDishProductRequest request, CancellationToken cancellationToken = default)
    {
        var link = await _context.DishProducts
            .FirstOrDefaultAsync(dp => dp.DishId == dishId && dp.ProductId == productId, cancellationToken);
        if (link is null)
        {
            return null;
        }

        link.Quantity = request.Quantity?.Trim();
        await _context.SaveChangesAsync(cancellationToken);

        return (await GetAsync(link.DishId, link.ProductId, cancellationToken))!;
    }

    public async Task<bool> DeleteAsync(int dishId, int productId, CancellationToken cancellationToken = default)
    {
        var link = await _context.DishProducts
            .FirstOrDefaultAsync(dp => dp.DishId == dishId && dp.ProductId == productId, cancellationToken);
        if (link is null)
        {
            return false;
        }

        _context.DishProducts.Remove(link);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task EnsureDishExists(int dishId, CancellationToken cancellationToken)
    {
        var exists = await _context.Dishes.AnyAsync(d => d.Id == dishId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Блюдо с идентификатором {dishId} не найдено.");
        }
    }

    private async Task EnsureProductExists(int productId, CancellationToken cancellationToken)
    {
        var exists = await _context.Products.AnyAsync(p => p.Id == productId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Продукт с идентификатором {productId} не найден.");
        }
    }

    private static DishProductDto MapToDto(DishProduct link)
        => new(
            link.DishId,
            link.Dish.Name,
            link.ProductId,
            link.Product.Name,
            link.Quantity);
}
