using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Application.DTOs.Dishes;

public class CreateDishRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Description { get; set; }

    [MaxLength(2000)]
    public string? Instructions { get; set; }

    [Range(0, int.MaxValue)]
    public int? PreparationMinutes { get; set; }

    [MaxLength(200)]
    public string? ImageUrl { get; set; }
}
