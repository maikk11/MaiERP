using MaiERP.Api.Dtos;
using MaiERP.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaiERP.Api.Controllers;

[ApiController]
[Route("api/items")]
[Produces("application/json")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ItemResponse>>> GetAll()
    {
        return Ok(await _itemService.GetAllAsync());
    }

    [HttpGet("{id:long}", Name = "GetItemById")]
    [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemResponse>> GetById(long id)
    {
        return Ok(await _itemService.GetAsync(id));
    }

    [HttpGet("code/{code}")]
    [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemResponse>> GetByCode(string code)
    {
        return Ok(await _itemService.GetByCodeAsync(code));
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<ItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ItemResponse>>> Search([FromQuery] string searchTerm)
    {
        return Ok(await _itemService.SearchAsync(searchTerm));
    }

    [HttpGet("category/{categoryId:long}")]
    [ProducesResponseType(typeof(IEnumerable<ItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ItemResponse>>> GetByCategory(long categoryId)
    {
        return Ok(await _itemService.GetByCategoryAsync(categoryId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ItemResponse>> Create(ItemRequest request)
    {
        var item = await _itemService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ItemResponse>> Update(long id, ItemRequest request)
    {
        return Ok(await _itemService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await _itemService.DeleteAsync(id);
        return NoContent();
    }
}
