using MaiERP.Api.Models.Items;

namespace MaiERP.Api.Dtos;

public class ItemResponse
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public SupplyType SupplyType { get; set; }
    public bool IsLotManaged { get; set; }
    public decimal MinimumStock { get; set; }
    public decimal SafetyStock { get; set; }
    public int LeadTimeDays { get; set; }
    public ItemCategoryResponse Category { get; set; } = new();
}