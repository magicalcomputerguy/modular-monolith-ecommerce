using Catalog.Products.Features.DeleteProduct;
using FluentValidation;

namespace Catalog.Products.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product identifier is required");
    }
}