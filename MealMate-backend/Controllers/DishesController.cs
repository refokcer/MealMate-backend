using MealMate_backend.Application.DTOs.Dishes;
using MealMate_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealMate_backend.Controllers;

[ApiController]
[Route("api/dishes")]
public class DishesController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishesController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DishDto>>> GetAll(CancellationToken cancellationToken)
    {
        var dishes = await _dishService.GetAllAsync(cancellationToken);
        return Ok(dishes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DishDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dish = await _dishService.GetByIdAsync(id, cancellationToken);
        return dish is null ? NotFound() : Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult<DishDto>> Create([FromBody] CreateDishRequest request, CancellationToken cancellationToken)
    {
        var dish = await _dishService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = dish.Id }, dish);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DishDto>> Update(int id, [FromBody] UpdateDishRequest request, CancellationToken cancellationToken)
    {
        var updated = await _dishService.UpdateAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _dishService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
