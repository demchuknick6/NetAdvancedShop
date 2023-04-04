namespace Catalog.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(v => v.ImageUri)
            .Must(x => string.IsNullOrEmpty(x) || x.IsValidUrl())
            .WithMessage("ImageUrl can be empty or must be a valid image URL.");
    }
}
