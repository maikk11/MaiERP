using MaiERP.Api.Common.Exceptions;
using MaiERP.Api.Dtos;
using MaiERP.Api.Models.Items;
using MaiERP.Api.Repositories;

namespace MaiERP.Api.Services;

public class ItemCategoryService : IItemCategoryService
{
    private readonly IItemCategoryRepository _itemCategoryRepository;
    private readonly IItemRepository _itemRepository;

    public ItemCategoryService(
        IItemCategoryRepository itemCategoryRepository,
        IItemRepository itemRepository)
    {
        _itemCategoryRepository = itemCategoryRepository;
        _itemRepository = itemRepository;
    }

    public async Task<IEnumerable<ItemCategoryResponse>> GetAllAsync()
    {
        var categories = await _itemCategoryRepository.GetAllAsync();
        return categories.Select(MapToResponse);
    }

    public async Task<ItemCategoryResponse> GetAsync(long id)
    {
        var category = await _itemCategoryRepository.GetAsync(id)
            ?? throw new NotFoundException($"Item category with id {id} was not found.");

        return MapToResponse(category);
    }

    public async Task<ItemCategoryResponse> GetByCodeAsync(string code)
    {
        var category = await _itemCategoryRepository.GetByCodeAsync(code)
            ?? throw new NotFoundException($"Item category with code '{code}' was not found.");

        return MapToResponse(category);
    }

    public async Task<IEnumerable<ItemCategoryResponse>> SearchAsync(string searchTerm)
    {
        var categories = await _itemCategoryRepository.SearchAsync(searchTerm);
        return categories.Select(MapToResponse);
    }

    public async Task<ItemCategoryResponse> CreateAsync(ItemCategoryRequest request)
    {
        await EnsureCodeIsAvailableAsync(request.Code);

        var category = await _itemCategoryRepository.AddAsync(new ItemCategory
        {
            Code = request.Code,
            Description = request.Description
        });

        return MapToResponse(category);
    }

    public async Task<ItemCategoryResponse> UpdateAsync(long id, ItemCategoryRequest request)
    {
        _ = await _itemCategoryRepository.GetAsync(id)
            ?? throw new NotFoundException($"Item category with id {id} was not found.");

        await EnsureCodeIsAvailableAsync(request.Code, id);

        var updatedCategory = await _itemCategoryRepository.UpdateAsync(new ItemCategory
        {
            Id = id,
            Code = request.Code,
            Description = request.Description
        }) ?? throw new NotFoundException($"Item category with id {id} was not found.");

        return MapToResponse(updatedCategory);
    }

    public async Task DeleteAsync(long id)
    {
        var category = await _itemCategoryRepository.GetAsync(id)
            ?? throw new NotFoundException($"Item category with id {id} was not found.");

        if ((await _itemRepository.GetByCategoryAsync(id)).Any())
        {
            throw new ConflictException(
                $"Item category with id {id} cannot be deleted because it contains items.");
        }

        _ = await _itemCategoryRepository.DeleteAsync(category.Id)
            ?? throw new NotFoundException($"Item category with id {id} was not found.");
    }

    private async Task EnsureCodeIsAvailableAsync(string code, long? excludedCategoryId = null)
    {
        var existingCategory = await _itemCategoryRepository.GetByCodeAsync(code);
        if (existingCategory is not null && existingCategory.Id != excludedCategoryId)
        {
            throw new ConflictException($"An item category with code '{code}' already exists.");
        }
    }

    private static ItemCategoryResponse MapToResponse(ItemCategory category)
    {
        return new ItemCategoryResponse
        {
            Id = category.Id,
            Code = category.Code,
            Description = category.Description
        };
    }
}
