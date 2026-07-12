using MealMate_backend.Application.DTOs.Shopping;
using MealMate_backend.Application.Interfaces;
using MealMate_backend.Domain.Entities;
using MealMate_backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate_backend.Application.Services;

public class ShoppingListService : IShoppingListService
{
    private readonly MealMateDbContext _context;

    public ShoppingListService(MealMateDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ShoppingListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await QueryItems()
            .AsNoTracking()
            .OrderBy(item => item.Product.Name)
            .ToListAsync(cancellationToken);

        return items.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyCollection<ShoppingListItemDto>> SaveAsync(SaveShoppingListRequest request, CancellationToken cancellationToken = default)
    {
        var productIds = request.Items.Select(item => item.ProductId).Distinct().ToList();
        await EnsureProductsExist(productIds, cancellationToken);

        var existing = await _context.ShoppingListItems.ToListAsync(cancellationToken);
        _context.ShoppingListItems.RemoveRange(existing);

        var mergedItems = request.Items
            .GroupBy(item => item.ProductId)
            .Select(group => new ShoppingListItem
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => item.Quantity ?? 0),
                IsChecked = group.All(item => item.IsChecked),
                Notes = string.Join("; ", group.Select(item => item.Notes?.Trim()).Where(note => !string.IsNullOrWhiteSpace(note)))
            })
            .ToList();

        await _context.ShoppingListItems.AddRangeAsync(mergedItems, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return await GetAllAsync(cancellationToken);
    }

    public async Task<ShoppingListItemDto> CreateItemAsync(CreateShoppingListItemRequest request, CancellationToken cancellationToken = default)
    {
        await EnsureProductsExist(new[] { request.ProductId }, cancellationToken);

        var existing = await _context.ShoppingListItems
            .FirstOrDefaultAsync(item => item.ProductId == request.ProductId, cancellationToken);

        if (existing is null)
        {
            existing = new ShoppingListItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Notes = request.Notes?.Trim()
            };

            await _context.ShoppingListItems.AddAsync(existing, cancellationToken);
        }
        else
        {
            existing.Quantity = (existing.Quantity ?? 0) + (request.Quantity ?? 0);
            existing.Notes = MergeNotes(existing.Notes, request.Notes);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return (await QueryItems()
            .AsNoTracking()
            .FirstAsync(item => item.Id == existing.Id, cancellationToken)
            .ContinueWith(task => MapToDto(task.Result), cancellationToken))!;
    }

    public async Task<ShoppingListItemDto?> UpdateItemAsync(int id, UpdateShoppingListItemRequest request, CancellationToken cancellationToken = default)
    {
        var item = await _context.ShoppingListItems.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (item is null)
        {
            return null;
        }

        item.Quantity = request.Quantity;
        item.IsChecked = request.IsChecked;
        item.Notes = request.Notes?.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return MapToDto(await QueryItems().AsNoTracking().FirstAsync(current => current.Id == id, cancellationToken));
    }

    public async Task<bool> DeleteItemAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.ShoppingListItems.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (item is null)
        {
            return false;
        }

        _context.ShoppingListItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        var items = await _context.ShoppingListItems.ToListAsync(cancellationToken);
        _context.ShoppingListItems.RemoveRange(items);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ShoppingListItemDto>> AddBaseProductsAsync(CancellationToken cancellationToken = default)
    {
        var baseProducts = await _context.BaseShoppingProducts.AsNoTracking().ToListAsync(cancellationToken);

        foreach (var baseProduct in baseProducts)
        {
            var existing = await _context.ShoppingListItems
                .FirstOrDefaultAsync(item => item.ProductId == baseProduct.ProductId, cancellationToken);

            if (existing is null)
            {
                await _context.ShoppingListItems.AddAsync(new ShoppingListItem
                {
                    ProductId = baseProduct.ProductId,
                    Quantity = baseProduct.Quantity,
                    Notes = baseProduct.Notes
                }, cancellationToken);
            }
            else
            {
                existing.Quantity = (existing.Quantity ?? 0) + (baseProduct.Quantity ?? 0);
                existing.Notes = MergeNotes(existing.Notes, baseProduct.Notes);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return await GetAllAsync(cancellationToken);
    }

    private IQueryable<ShoppingListItem> QueryItems()
        => _context.ShoppingListItems.Include(item => item.Product);

    private async Task EnsureProductsExist(IEnumerable<int> productIds, CancellationToken cancellationToken)
    {
        var distinctIds = productIds.Distinct().ToList();
        var existingCount = await _context.Products.CountAsync(product => distinctIds.Contains(product.Id), cancellationToken);
        if (existingCount != distinctIds.Count)
        {
            throw new KeyNotFoundException("Один или несколько продуктов не найдены.");
        }
    }

    private static string? MergeNotes(string? current, string? next)
    {
        var normalizedNext = next?.Trim();
        if (string.IsNullOrWhiteSpace(normalizedNext))
        {
            return current;
        }

        if (string.IsNullOrWhiteSpace(current))
        {
            return normalizedNext;
        }

        return $"{current}; {normalizedNext}";
    }

    private static ShoppingListItemDto MapToDto(ShoppingListItem item)
        => new(
            item.Id,
            item.ProductId,
            item.Product.Name,
            item.Product.Unit,
            item.Product.Category,
            item.Quantity,
            item.IsChecked,
            item.Notes);
}
