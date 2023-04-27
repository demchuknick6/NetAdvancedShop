namespace Catalog.WebUI.Controllers;

public class CategoriesController : ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly LinkGenerator _linkGenerator;

    public CategoriesController(ISender mediator, LinkGenerator linkGenerator)
    {
        _mediator = mediator;
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    public async Task<LinkedCollectionResourceDto<CategoryResourceDto>> GetCategories()
    {
        var categories = (await _mediator.Send(new GetCategoriesQuery()))
            .Select(c => new CategoryResourceDto(c, GenerateLinksForCategory(c.Id)));
        var links = GenerateLinksForCategories();

        return new LinkedCollectionResourceDto<CategoryResourceDto>(categories, links);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CategoryResourceDto> GetCategory(int id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));
        var links = GenerateLinksForCategory(category.Id);

        return new CategoryResourceDto(category, links);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<int> Create(CreateCategoryModel model) =>
        await _mediator.Send(new CreateCategoryCommand(model.Name, model.ImageUri, model.ParentCategoryId));

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task Update(int id, UpdateCategoryModel model) =>
        await _mediator.Send(new UpdateCategoryCommand(id, model.Name, model.ImageUri, model.ParentCategoryId));

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task Delete(int id) => await _mediator.Send(new DeleteCategoryCommand(id));

    private IEnumerable<LinkDto> GenerateLinksForCategory(int id) =>
        new List<LinkDto>
        {
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(GetCategory), values: new { id })!,
                "self",
                "GET"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(Delete), values: new { id })!,
                "delete_category",
                "DELETE"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(Update), values: new { id })!,
                "update_category",
                "PUT")
        };

    private IEnumerable<LinkDto> GenerateLinksForCategories() =>
        new List<LinkDto>
        {
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(GetCategories))!,
                "self",
                "GET")
        };
}
