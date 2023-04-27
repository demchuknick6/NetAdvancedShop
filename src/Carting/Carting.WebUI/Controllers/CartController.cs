namespace Carting.WebUI.Controllers;

[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class CartController : ApiVersionedControllerBase
{
    private readonly ISender _mediator;

    public CartController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get cart information. Returns a cart model (cart key + list of cart items).
    /// </summary>
    /// <param name="id">Cart unique key.</param>
    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CartModel> GetCartV1(Guid id) =>
        new()
        {
            Id = id,
            Items = (await _mediator.Send(new GetCartItemsQuery(id))).ToList()
        };

    /// <summary>
    /// Get cart information. Returns a list of cart items.
    /// </summary>
    /// <param name="id">Cart unique key.</param>
    [HttpGet("{id}")]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IReadOnlyCollection<CartItemDto>> GetCartV2(Guid id) =>
        await _mediator.Send(new GetCartItemsQuery(id));

    /// <summary>
    /// Add item to cart. Returns 200 if item was added to the cart.
    /// If there was no cart for specified key – creates it. Otherwise returns a corresponding HTTP code.
    /// </summary>
    /// <param name="id">Cart unique key.</param>
    /// <param name="model">Request model.</param>
    [HttpPost("{id}/items")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task AddCartItem(Guid id, AddCartItemModel model) =>
        await _mediator.Send(new AddCartItemCommand(id, new AddCartItemDto
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Quantity = model.Quantity,
            Image = model.Image == null
                ? null
                : new AddCartItemImageModel { Uri = model.Image.Uri, AltText = model.Image.AltText }
        }));

    /// <summary>
    /// Delete item from cart. Returns 200 if item was deleted, otherwise returns corresponding HTTP code.
    /// </summary>
    /// <param name="id">Cart unique key.</param>
    /// <param name="itemId">Item id.</param>
    [HttpDelete("{id}/items/{itemId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task RemoveCartItem(Guid id, int itemId) =>
        await _mediator.Send(new RemoveCartItemCommand(id, itemId));
}
