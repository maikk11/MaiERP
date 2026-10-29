using MaiERP.Api.Models.Items;
namespace MaiERP.Api.Repositories;

public interface IItemRepository
{
   Task<IEnumerable<Item>> GetAllAsync();
   Task<Item?> GetAsync(long id);
   Task<Item?> GetByCodeAsync(string code);
   Task<IEnumerable<Item>> SearchAsync(string searchTerm);
   Task<Item> AddAsync(Item item);
   Task<Item?> UpdateAsync(Item item);
   Task<Item?> DeleteAsync(long id);
   Task<IEnumerable<Item>> GetByCategoryAsync(long categoryId);
}