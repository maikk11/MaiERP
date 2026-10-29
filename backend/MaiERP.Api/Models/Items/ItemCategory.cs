using System.ComponentModel.DataAnnotations;

namespace MaiERP.Api.Models.Items;

public class ItemCategory
{
    public long Id { get; set;}
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)] 
    public string Description { get; set;} = string.Empty;
}