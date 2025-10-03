using MealMate_backend.Application.DTOs.MealGroups;
using MealMate_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealMate_backend.Controllers;

[ApiController]
[Route("api/meal-groups")]
public class MealGroupsController : ControllerBase
{
    private readonly IMealGroupService _mealGroupService;

    public MealGroupsController(IMealGroupService mealGroupService)
    {
        _mealGroupService = mealGroupService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<MealGroupDto>>> GetAll(CancellationToken cancellationToken)
    {
        var groups = await _mealGroupService.GetAllAsync(cancellationToken);
        return Ok(groups);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MealGroupDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var group = await _mealGroupService.GetByIdAsync(id, cancellationToken);
        return group is null ? NotFound() : Ok(group);
    }

    [HttpPost]
    public async Task<ActionResult<MealGroupDto>> Create([FromBody] CreateMealGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await _mealGroupService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MealGroupDto>> Update(int id, [FromBody] UpdateMealGroupRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mealGroupService.UpdateAsync(id, request, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _mealGroupService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
