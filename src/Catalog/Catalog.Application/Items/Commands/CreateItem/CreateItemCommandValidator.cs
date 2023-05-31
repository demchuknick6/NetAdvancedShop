namespace NetAdvancedShop.Catalog.Application.Items.Commands.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(v => v.ImageUri)
            .Must(x => string.IsNullOrEmpty(x) || x.IsValidUrl())
            .WithMessage("ImageUrl can be empty or must be a valid image URL.");

        RuleFor(x => x.Description)
            .Must(x => string.IsNullOrEmpty(x) || IsValidInput(x))
            .WithMessage("Description can be empty or contain valid HTML or plain text, but cannot contain potentially dangerous input.");
    }

    private bool IsValidInput(string input)
    {
        return input.IsValidHtml()
            ? !input.ContainsXssScript()
            : !HttpUtility.HtmlEncode(input).ContainsXssScript();
    }
}
