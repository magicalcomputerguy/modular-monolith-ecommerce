using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Models;
using MediatR;

namespace Catalog.Products.Features.CreateProduct;

public sealed record CreateProductCommand(ProductDto Product) : IRequest<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

internal sealed class CreateProductHandler(CatalogDbContext dbContext) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(request.Product);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(Guid.NewGuid(), productDto.Name, productDto.Category, productDto.Description,
            productDto.ImageFile, productDto.Price);
        
        return product;
    }
}