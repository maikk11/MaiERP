namespace MaiERP.Api.Dtos;

public class ItemCategoryResponse
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}