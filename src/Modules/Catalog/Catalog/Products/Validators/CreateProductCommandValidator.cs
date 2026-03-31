using Catalog.Products.Features.CreateProduct;
using FluentValidation;

namespace Catalog.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Product name is required");
        RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("Image is required");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price is required and must be greater than 0");
    }
}