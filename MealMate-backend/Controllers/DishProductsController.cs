using MealMate_backend.Application.DTOs.DishProducts;
using MealMate_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealMate_backend.Controllers;

[ApiController]
[Route("api/dish-products")]
public class DishProductsController : ControllerBase
{
    private readonly IDishProductService _dishProductService;

    public DishProductsController(IDishProductService dishProductService)
    {
        _dishProductService = dishProductService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DishProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var links = await _dishProductService.GetAllAsync(cancellationToken);
        return Ok(links);
    }

    [HttpGet("{dishId:int}/{productId:int}")]
    public async Task<ActionResult<DishProductDto>> Get(int dishId, int productId, CancellationToken cancellationToken)
    {
        var link = await _dishProductService.GetAsync(dishId, productId, cancellationToken);
        return link is null ? NotFound() : Ok(link);
    }

    [HttpPost]
    public async Task<ActionResult<DishProductDto>> Create([FromBody] CreateDishProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var link = await _dishProductService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(Get), new { dishId = link.DishId, productId = link.ProductId }, link);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{dishId:int}/{productId:int}")]
    public async Task<ActionResult<DishProductDto>> Update(int dishId, int productId, [FromBody] UpdateDishProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var link = await _dishProductService.UpdateAsync(dishId, productId, request, cancellationToken);
            return link is null ? NotFound() : Ok(link);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{dishId:int}/{productId:int}")]
    public async Task<IActionResult> Delete(int dishId, int productId, CancellationToken cancellationToken)
    {
        var deleted = await _dishProductService.DeleteAsync(dishId, productId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
