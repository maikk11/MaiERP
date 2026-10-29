using MaiERP.Api.Models.Items;
namespace MaiERP.Api.Repositories;


public interface IItemCategoryRepository
{
    Task<IEnumerable<ItemCategory>> GetAllAsync();
    Task<ItemCategory?> GetAsync(long id);
    Task<ItemCategory?> GetByCodeAsync(string code);
    Task<IEnumerable<ItemCategory>> SearchAsync(string searchTerm);
    Task<ItemCategory> AddAsync(ItemCategory category);
    Task<ItemCategory?> UpdateAsync(ItemCategory category);
    Task<ItemCategory?> DeleteAsync(long id);
}