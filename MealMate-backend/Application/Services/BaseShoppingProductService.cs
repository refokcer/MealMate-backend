using MealMate_backend.Application.DTOs.Shopping;
using MealMate_backend.Application.Interfaces;
using MealMate_backend.Domain.Entities;
using MealMate_backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate_backend.Application.Services;

public class BaseShoppingProductService : IBaseShoppingProductService
{
    private readonly MealMateDbContext _context;

    public BaseShoppingProductService(MealMateDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<BaseShoppingProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await QueryItems()
            .AsNoTracking()
            .OrderBy(item => item.Product.Name)
            .ToListAsync(cancellationToken);

        return items.Select(MapToDto).ToList();
    }

    public async Task<BaseShoppingProductDto> CreateAsync(CreateBaseShoppingProductRequest request, CancellationToken cancellationToken = default)
    {
        await EnsureProductExists(request.ProductId, cancellationToken);

        var exists = await _context.BaseShoppingProducts
            .AnyAsync(item => item.ProductId == request.ProductId, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Этот продукт уже есть в базовых покупках.");
        }

        var item = new BaseShoppingProduct
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Notes = request.Notes?.Trim()
        };

        await _context.BaseShoppingProducts.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(await QueryItems().AsNoTracking().FirstAsync(current => current.Id == item.Id, cancellationToken));
    }

    public async Task<BaseShoppingProductDto?> UpdateAsync(int id, UpdateBaseShoppingProductRequest request, CancellationToken cancellationToken = default)
    {
        await EnsureProductExists(request.ProductId, cancellationToken);

        var duplicate = await _context.BaseShoppingProducts
            .AnyAsync(item => item.Id != id && item.ProductId == request.ProductId, cancellationToken);
        if (duplicate)
        {
            throw new InvalidOperationException("Этот продукт уже есть в базовых покупках.");
        }

        var item = await _context.BaseShoppingProducts.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (item is null)
        {
            return null;
        }

        item.ProductId = request.ProductId;
        item.Quantity = request.Quantity;
        item.Notes = request.Notes?.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(await QueryItems().AsNoTracking().FirstAsync(current => current.Id == id, cancellationToken));
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.BaseShoppingProducts.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (item is null)
        {
            return false;
        }

        _context.BaseShoppingProducts.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private IQueryable<BaseShoppingProduct> QueryItems()
        => _context.BaseShoppingProducts.Include(item => item.Product);

    private async Task EnsureProductExists(int productId, CancellationToken cancellationToken)
    {
        var exists = await _context.Products.AnyAsync(product => product.Id == productId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException("Продукт не найден.");
        }
    }

    private static BaseShoppingProductDto MapToDto(BaseShoppingProduct item)
        => new(
            item.Id,
            item.ProductId,
            item.Product.Name,
            item.Product.Unit,
            item.Product.Category,
            item.Quantity,
            item.Notes);
}
