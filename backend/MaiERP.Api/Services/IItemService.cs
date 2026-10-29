using MaiERP.Api.Dtos;

namespace MaiERP.Api.Services;

public interface IItemService
{
    Task<IEnumerable<ItemResponse>> GetAllAsync();
    Task<ItemResponse> GetAsync(long id);
    Task<ItemResponse> GetByCodeAsync(string code);
    Task<IEnumerable<ItemResponse>> SearchAsync(string searchTerm);
    Task<IEnumerable<ItemResponse>> GetByCategoryAsync(long categoryId);
    Task<ItemResponse> CreateAsync(ItemRequest request);
    Task<ItemResponse> UpdateAsync(long id, ItemRequest request);
    Task DeleteAsync(long id);
}
