using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Models;
using MediatR;

namespace Catalog.Products.Features.UpdateProduct;

public sealed record UpdateProductCommand(ProductDto Product) : IRequest<UpdateProductResult>;

public sealed record UpdateProductResult(bool Success);

internal sealed class UpdateProductHandler(CatalogDbContext dbContext) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([request.Product.Id], cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product not found with id {request.Product.Id}");
        }
        
        UpdateProduct(product, request.Product);
        
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true);
    }

    private void UpdateProduct(Product product, ProductDto productDto)
    {
        product.Update(productDto.Name, productDto.Category, productDto.Description, productDto.ImageFile, productDto.Price);
    }
}