namespace Catalog.WebUI.Controllers;

public class ItemsController : ApiControllerBase
{
    private readonly ISender _mediator;

    public ItemsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<ItemDto>> GetItems([FromQuery] GetItemsWithPaginationQuery query) =>
        await _mediator.Send(query);

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ItemDto> GetItem(int id) =>
        await _mediator.Send(new GetItemByIdQuery(id));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<int> Create(CreateItemModel model) =>
        await _mediator.Send(new CreateItemCommand(model.Name, model.Description,
            model.ImageUri, model.CategoryId, model.Price, model.Amount));

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task Update(int id, UpdateItemModel model) =>
        await _mediator.Send(
            new UpdateItemCommand(id, model.Name, model.Description,
                model.ImageUri, model.CategoryId, model.Price, model.Amount));

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task Delete(int id) => await _mediator.Send(new DeleteItemCommand(id));
}
