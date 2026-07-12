using MealMate_backend.Application.DTOs.Shopping;
using MealMate_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealMate_backend.Controllers;

[ApiController]
[Route("api/base-shopping-products")]
public class BaseShoppingProductsController : ControllerBase
{
    private readonly IBaseShoppingProductService _baseShoppingProductService;

    public BaseShoppingProductsController(IBaseShoppingProductService baseShoppingProductService)
    {
        _baseShoppingProductService = baseShoppingProductService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<BaseShoppingProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _baseShoppingProductService.GetAllAsync(cancellationToken);
        return Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult<BaseShoppingProductDto>> Create([FromBody] CreateBaseShoppingProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _baseShoppingProductService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = item.Id }, item);
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

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BaseShoppingProductDto>> Update(int id, [FromBody] UpdateBaseShoppingProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _baseShoppingProductService.UpdateAsync(id, request, cancellationToken);
            return item is null ? NotFound() : Ok(item);
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _baseShoppingProductService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
