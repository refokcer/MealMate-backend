using System.ComponentModel.DataAnnotations;

namespace MealMate_backend.Application.DTOs.Products;

public class CreateProductRequest
{
    [Required]
    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(40)]
    public string? Category { get; set; }

    [MaxLength(200)]
    public string? Notes { get; set; }
}
