using System.ComponentModel.DataAnnotations;

namespace MaiERP.Api.Dtos;

public class ItemCategoryRequest
{
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;
}