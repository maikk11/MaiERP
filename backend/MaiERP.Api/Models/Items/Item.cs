using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiERP.Api.Models.Items;

public class Item
{
    public long Id { get; set;}
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)] 
    public string Description { get; set;} = string.Empty;
    [Required]
    [MaxLength(2)] 
    public string UnitOfMeasure { get;set; } = string.Empty;
    public SupplyType SupplyType { get; set; }
    public bool IsLotManaged { get; set; }
    public decimal MinimumStock { get ;set; }
    public decimal SafetyStock { get ;set; }
    public int LeadTimeDays { get; set; } 
    public long ItemCategoryId { get; set; }
    [ForeignKey("ItemCategoryId")]
    public ItemCategory ItemCategory { get; set; } = null!;
}