using MaiERP.Api.Dtos;

namespace MaiERP.Api.Services;

public interface IItemCategoryService
{
    Task<IEnumerable<ItemCategoryResponse>> GetAllAsync();
    Task<ItemCategoryResponse> GetAsync(long id);
    Task<ItemCategoryResponse> GetByCodeAsync(string code);
    Task<IEnumerable<ItemCategoryResponse>> SearchAsync(string searchTerm);
    Task<ItemCategoryResponse> CreateAsync(ItemCategoryRequest request);
    Task<ItemCategoryResponse> UpdateAsync(long id, ItemCategoryRequest request);
    Task DeleteAsync(long id);
}
