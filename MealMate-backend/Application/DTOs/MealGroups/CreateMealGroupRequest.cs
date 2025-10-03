using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Application.DTOs.MealGroups;

public class CreateMealGroupRequest
{
    [Required]
    [MaxLength(60)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    [MaxLength(30)]
    public string? AccentColor { get; set; }
}
