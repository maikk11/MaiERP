using MaiERP.Api.Dtos;
using MaiERP.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaiERP.Api.Controllers;

[ApiController]
[Route("api/item-categories")]
[Produces("application/json")]
public class ItemCategoriesController : ControllerBase
{
    private readonly IItemCategoryService _itemCategoryService;

    public ItemCategoriesController(IItemCategoryService itemCategoryService)
    {
        _itemCategoryService = itemCategoryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ItemCategoryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ItemCategoryResponse>>> GetAll()
    {
        return Ok(await _itemCategoryService.GetAllAsync());
    }

    [HttpGet("{id:long}", Name = "GetItemCategoryById")]
    [ProducesResponseType(typeof(ItemCategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemCategoryResponse>> GetById(long id)
    {
        return Ok(await _itemCategoryService.GetAsync(id));
    }

    [HttpGet("code/{code}")]
    [ProducesResponseType(typeof(ItemCategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemCategoryResponse>> GetByCode(string code)
    {
        return Ok(await _itemCategoryService.GetByCodeAsync(code));
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<ItemCategoryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ItemCategoryResponse>>> Search([FromQuery] string searchTerm)
    {
        return Ok(await _itemCategoryService.SearchAsync(searchTerm));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemCategoryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ItemCategoryResponse>> Create(ItemCategoryRequest request)
    {
        var category = await _itemCategoryService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(ItemCategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ItemCategoryResponse>> Update(long id, ItemCategoryRequest request)
    {
        return Ok(await _itemCategoryService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(long id)
    {
        await _itemCategoryService.DeleteAsync(id);
        return NoContent();
    }
}
