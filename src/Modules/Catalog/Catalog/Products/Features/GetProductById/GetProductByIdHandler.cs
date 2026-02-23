using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Products.Features.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResult>;

public sealed record GetProductByIdResult(ProductDto Product);

internal sealed class GetProductByIdHandler(CatalogDbContext dbContext) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        
        if (product is null)
        {
            throw new Exception($"Product not found with id {request.Id}");
        }
        
        var productDto = product.Adapt<ProductDto>();
        
        return new GetProductByIdResult(productDto);
    }
}