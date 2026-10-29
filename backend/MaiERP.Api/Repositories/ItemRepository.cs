using MaiERP.Api.Models.Items;
using MaiERP.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MaiERP.Api.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _appDbContext;
    public ItemRepository(AppDbContext appDbContext){
        _appDbContext = appDbContext;
    }
    public async Task<Item> AddAsync(Item item)
    {
        await _appDbContext.Items.AddAsync(item);
        await _appDbContext.SaveChangesAsync();
        await _appDbContext.Entry(item).Reference(i => i.ItemCategory).LoadAsync();
        return item;
    }

    public async Task<Item?> DeleteAsync(long id)
    {
        var existingItem = await _appDbContext.Items.FindAsync(id);
        if(existingItem != null)
        {
            _appDbContext.Items.Remove(existingItem);
            await _appDbContext.SaveChangesAsync();
            return existingItem;
        }
        return null;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _appDbContext.Items
            .Include(i => i.ItemCategory)
            .ToListAsync();
    }

    public async Task<Item?> GetAsync(long id)
    {
        return await _appDbContext.Items
            .Include(i => i.ItemCategory)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Item>> GetByCategoryAsync(long categoryId)
    {
        return await _appDbContext.Items
            .Where(i => i.ItemCategoryId == categoryId)
            .Include(i => i.ItemCategory)
            .ToListAsync();
    }

    public async Task<Item?> GetByCodeAsync(string code)
    {
        return await _appDbContext.Items
            .Include(i => i.ItemCategory)
            .FirstOrDefaultAsync(i => i.Code == code);
    }

    public async Task<IEnumerable<Item>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<Item>();
        }
        var pattern = $"%{searchTerm}%";
        return await _appDbContext.Items
            .Include(i => i.ItemCategory)
            .Where(i =>
                EF.Functions.Like(i.Code, pattern) ||
                EF.Functions.Like(i.Description, pattern))
                .ToListAsync();
    }

    public async Task<Item?> UpdateAsync(Item item)
    {
        var existingItem = await _appDbContext.Items.FindAsync(item.Id);
        if(existingItem != null)
        {
            existingItem.Code = item.Code;
            existingItem.Description = item.Description;
            existingItem.UnitOfMeasure = item.UnitOfMeasure;
            existingItem.SupplyType = item.SupplyType;
            existingItem.IsLotManaged = item.IsLotManaged;
            existingItem.MinimumStock = item.MinimumStock;
            existingItem.SafetyStock = item.SafetyStock;
            existingItem.LeadTimeDays = item.LeadTimeDays;
            existingItem.ItemCategoryId= item.ItemCategoryId;
            await _appDbContext.SaveChangesAsync();
            await _appDbContext.Entry(existingItem).Reference(i => i.ItemCategory).LoadAsync();
            return existingItem;
        }
        return null;
    }
}