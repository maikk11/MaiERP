using System.ComponentModel.DataAnnotations;
using MaiERP.Api.Models.Items;

namespace MaiERP.Api.Dtos;

public class ItemRequest
{
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(2)]
    public string UnitOfMeasure { get; set; } = string.Empty;

    public SupplyType SupplyType { get; set; }

    public bool IsLotManaged { get; set; }

    [Range(0, double.MaxValue)]
    public decimal MinimumStock { get; set; }

    [Range(0, double.MaxValue)]
    public decimal SafetyStock { get; set; }

    [Range(0, int.MaxValue)]
    public int LeadTimeDays { get; set; }

    [Range(1, double.MaxValue)]
    public long ItemCategoryId { get; set; }
}