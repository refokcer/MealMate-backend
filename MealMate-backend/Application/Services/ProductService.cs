using MealMate_backend.Application.DTOs.Products;
using MealMate_backend.Application.Interfaces;
using MealMate_backend.Domain.Entities;
using MealMate_backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MealMate_backend.Application.Services;

public class ProductService : IProductService
{
    private readonly MealMateDbContext _context;

    public ProductService(MealMateDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Include(p => p.DishProducts)
                .ThenInclude(dp => dp.Dish)
            .ToListAsync(cancellationToken);

        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.DishProducts)
                .ThenInclude(dp => dp.Dish)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return product is null ? null : MapToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = new Product
        {
            Name = request.Name.Trim(),
            Category = request.Category?.Trim(),
            Notes = request.Notes?.Trim()
        };

        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(product.Id, cancellationToken))!;
    }

    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (product is null)
        {
            return null;
        }

        product.Name = request.Name.Trim();
        product.Category = request.Category?.Trim();
        product.Notes = request.Notes?.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return (await GetByIdAsync(product.Id, cancellationToken))!;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (product is null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static ProductDto MapToDto(Product product)
    {
        var dishes = product.DishProducts
            .Select(dp => new ProductDishSummaryDto(
                dp.DishId,
                dp.Dish.Name,
                dp.Quantity))
            .ToList();

        return new ProductDto(
            product.Id,
            product.Name,
            product.Category,
            product.Notes,
            dishes);
    }
}
