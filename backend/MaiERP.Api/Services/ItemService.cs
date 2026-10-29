using MaiERP.Api.Common.Exceptions;
using MaiERP.Api.Dtos;
using MaiERP.Api.Models.Items;
using MaiERP.Api.Repositories;

namespace MaiERP.Api.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IItemCategoryRepository _itemCategoryRepository;

    public ItemService(
        IItemRepository itemRepository,
        IItemCategoryRepository itemCategoryRepository)
    {
        _itemRepository = itemRepository;
        _itemCategoryRepository = itemCategoryRepository;
    }

    public async Task<IEnumerable<ItemResponse>> GetAllAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        return items.Select(MapToResponse);
    }

    public async Task<ItemResponse> GetAsync(long id)
    {
        var item = await _itemRepository.GetAsync(id)
            ?? throw new NotFoundException($"Item with id {id} was not found.");

        return MapToResponse(item);
    }

    public async Task<ItemResponse> GetByCodeAsync(string code)
    {
        var item = await _itemRepository.GetByCodeAsync(code)
            ?? throw new NotFoundException($"Item with code '{code}' was not found.");

        return MapToResponse(item);
    }

    public async Task<IEnumerable<ItemResponse>> SearchAsync(string searchTerm)
    {
        var items = await _itemRepository.SearchAsync(searchTerm);
        return items.Select(MapToResponse);
    }

    public async Task<IEnumerable<ItemResponse>> GetByCategoryAsync(long categoryId)
    {
        var items = await _itemRepository.GetByCategoryAsync(categoryId);
        return items.Select(MapToResponse);
    }

    public async Task<ItemResponse> CreateAsync(ItemRequest request)
    {
        await EnsureCodeIsAvailableAsync(request.Code);
        await EnsureCategoryExistsAsync(request.ItemCategoryId);

        var item = await _itemRepository.AddAsync(new Item
        {
            Code = request.Code,
            Description = request.Description,
            UnitOfMeasure = request.UnitOfMeasure,
            SupplyType = request.SupplyType,
            IsLotManaged = request.IsLotManaged,
            MinimumStock = request.MinimumStock,
            SafetyStock = request.SafetyStock,
            LeadTimeDays = request.LeadTimeDays,
            ItemCategoryId = request.ItemCategoryId
        });

        return MapToResponse(item);
    }

    public async Task<ItemResponse> UpdateAsync(long id, ItemRequest request)
    {
        _ = await _itemRepository.GetAsync(id)
            ?? throw new NotFoundException($"Item with id {id} was not found.");

        await EnsureCodeIsAvailableAsync(request.Code, id);
        await EnsureCategoryExistsAsync(request.ItemCategoryId);

        var updatedItem = await _itemRepository.UpdateAsync(new Item
        {
            Id = id,
            Code = request.Code,
            Description = request.Description,
            UnitOfMeasure = request.UnitOfMeasure,
            SupplyType = request.SupplyType,
            IsLotManaged = request.IsLotManaged,
            MinimumStock = request.MinimumStock,
            SafetyStock = request.SafetyStock,
            LeadTimeDays = request.LeadTimeDays,
            ItemCategoryId = request.ItemCategoryId
        }) ?? throw new NotFoundException($"Item with id {id} was not found.");

        return MapToResponse(updatedItem);
    }

    public async Task DeleteAsync(long id)
    {
        var deletedItem = await _itemRepository.DeleteAsync(id);
        if (deletedItem is null)
        {
            throw new NotFoundException($"Item with id {id} was not found.");
        }
    }

    private async Task EnsureCodeIsAvailableAsync(string code, long? excludedItemId = null)
    {
        var existingItem = await _itemRepository.GetByCodeAsync(code);
        if (existingItem is not null && existingItem.Id != excludedItemId)
        {
            throw new ConflictException($"An item with code '{code}' already exists.");
        }
    }

    private async Task EnsureCategoryExistsAsync(long categoryId)
    {
        if (await _itemCategoryRepository.GetAsync(categoryId) is null)
        {
            throw new NotFoundException($"Item category with id {categoryId} was not found.");
        }
    }

    private static ItemResponse MapToResponse(Item item)
    {
        return new ItemResponse
        {
            Id = item.Id,
            Code = item.Code,
            Description = item.Description,
            UnitOfMeasure = item.UnitOfMeasure,
            SupplyType = item.SupplyType,
            IsLotManaged = item.IsLotManaged,
            MinimumStock = item.MinimumStock,
            SafetyStock = item.SafetyStock,
            LeadTimeDays = item.LeadTimeDays,
            Category = new ItemCategoryResponse
            {
                Id = item.ItemCategory.Id,
                Code = item.ItemCategory.Code,
                Description = item.ItemCategory.Description
            }
        };
    }
}
