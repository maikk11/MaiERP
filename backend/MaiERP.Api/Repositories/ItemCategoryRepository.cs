using MaiERP.Api.Data;
using MaiERP.Api.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace MaiERP.Api.Repositories;

public class ItemCategoryRepository : IItemCategoryRepository
{
    private readonly AppDbContext _appDbContext;

    public ItemCategoryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ItemCategory> AddAsync(ItemCategory category)
    {
        await _appDbContext.ItemCategories.AddAsync(category);
        await _appDbContext.SaveChangesAsync();
        return category;
    }

    public async Task<ItemCategory?> DeleteAsync(long id)
    {
        var existingCategory = await _appDbContext.ItemCategories.FindAsync(id);
        if (existingCategory != null)
        {
            _appDbContext.ItemCategories.Remove(existingCategory);
            await _appDbContext.SaveChangesAsync();
            return existingCategory;
        }
        return null;
    }

    public async Task<IEnumerable<ItemCategory>> GetAllAsync()
    {
        return await _appDbContext.ItemCategories.ToListAsync();
    }

    public async Task<ItemCategory?> GetAsync(long id)
    {
        return await _appDbContext.ItemCategories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ItemCategory?> GetByCodeAsync(string code)
    {
        return await _appDbContext.ItemCategories.FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<IEnumerable<ItemCategory>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<ItemCategory>();
        }
        var pattern = $"%{searchTerm}%";
        return await _appDbContext.ItemCategories
            .Where(c =>
                EF.Functions.Like(c.Code, pattern) ||
                EF.Functions.Like(c.Description, pattern))
            .ToListAsync();
    }

    public async Task<ItemCategory?> UpdateAsync(ItemCategory category)
    {
        var existingCategory = await _appDbContext.ItemCategories.FindAsync(category.Id);
        if (existingCategory != null)
        {
            existingCategory.Code = category.Code;
            existingCategory.Description = category.Description;
            await _appDbContext.SaveChangesAsync();
            return existingCategory;
        }
        return null;
    }
}