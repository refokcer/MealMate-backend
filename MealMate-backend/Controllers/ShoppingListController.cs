using MealMate_backend.Application.DTOs.Shopping;
using MealMate_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealMate_backend.Controllers;

[ApiController]
[Route("api/shopping-list")]
public class ShoppingListController : ControllerBase
{
    private readonly IShoppingListService _shoppingListService;

    public ShoppingListController(IShoppingListService shoppingListService)
    {
        _shoppingListService = shoppingListService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ShoppingListItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var items = await _shoppingListService.GetAllAsync(cancellationToken);
        return Ok(items);
    }

    [HttpPut]
    public async Task<ActionResult<IReadOnlyCollection<ShoppingListItemDto>>> Save([FromBody] SaveShoppingListRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var items = await _shoppingListService.SaveAsync(request, cancellationToken);
            return Ok(items);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("items")]
    public async Task<ActionResult<ShoppingListItemDto>> CreateItem([FromBody] CreateShoppingListItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _shoppingListService.CreateItemAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = item.Id }, item);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("items/{id:int}")]
    public async Task<ActionResult<ShoppingListItemDto>> UpdateItem(int id, [FromBody] UpdateShoppingListItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _shoppingListService.UpdateItemAsync(id, request, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("items/{id:int}")]
    public async Task<IActionResult> DeleteItem(int id, CancellationToken cancellationToken)
    {
        var deleted = await _shoppingListService.DeleteItemAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear(CancellationToken cancellationToken)
    {
        await _shoppingListService.ClearAsync(cancellationToken);
        return NoContent();
    }

    [HttpPost("add-base-products")]
    public async Task<ActionResult<IReadOnlyCollection<ShoppingListItemDto>>> AddBaseProducts(CancellationToken cancellationToken)
    {
        var items = await _shoppingListService.AddBaseProductsAsync(cancellationToken);
        return Ok(items);
    }
}
