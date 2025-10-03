using MealMate_backend.Application.DTOs.MealGroupDishes;
using MealMate_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealMate_backend.Controllers;

[ApiController]
[Route("api/meal-group-dishes")]
public class MealGroupDishesController : ControllerBase
{
    private readonly IMealGroupDishService _mealGroupDishService;

    public MealGroupDishesController(IMealGroupDishService mealGroupDishService)
    {
        _mealGroupDishService = mealGroupDishService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<MealGroupDishDto>>> GetAll(CancellationToken cancellationToken)
    {
        var links = await _mealGroupDishService.GetAllAsync(cancellationToken);
        return Ok(links);
    }

    [HttpGet("{mealGroupId:int}/{dishId:int}")]
    public async Task<ActionResult<MealGroupDishDto>> Get(int mealGroupId, int dishId, CancellationToken cancellationToken)
    {
        var link = await _mealGroupDishService.GetAsync(mealGroupId, dishId, cancellationToken);
        return link is null ? NotFound() : Ok(link);
    }

    [HttpPost]
    public async Task<ActionResult<MealGroupDishDto>> Create([FromBody] CreateMealGroupDishRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var link = await _mealGroupDishService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(Get), new { mealGroupId = link.MealGroupId, dishId = link.DishId }, link);
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

    [HttpPut("{mealGroupId:int}/{dishId:int}")]
    public async Task<ActionResult<MealGroupDishDto>> Update(int mealGroupId, int dishId, [FromBody] UpdateMealGroupDishRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var link = await _mealGroupDishService.UpdateAsync(mealGroupId, dishId, request, cancellationToken);
            return link is null ? NotFound() : Ok(link);
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

    [HttpDelete("{mealGroupId:int}/{dishId:int}")]
    public async Task<IActionResult> Delete(int mealGroupId, int dishId, CancellationToken cancellationToken)
    {
        var deleted = await _mealGroupDishService.DeleteAsync(mealGroupId, dishId, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
