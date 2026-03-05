namespace BlazorCrudLearning.Models;

/// <summary>
/// Represents a product in our tiny sample domain.
/// DataAnnotations are used by Blazor's EditForm for validation messages.
/// </summary>
public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(80, ErrorMessage = "Name can be at most 80 characters.")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 99999, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [StringLength(250, ErrorMessage = "Description can be at most 250 characters.")]
    public string? Description { get; set; }
}
