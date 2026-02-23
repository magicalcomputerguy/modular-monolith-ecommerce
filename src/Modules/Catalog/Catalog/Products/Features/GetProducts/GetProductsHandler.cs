using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Products.Features.GetProducts;

public sealed record GetProductsQuery() : IRequest<GetProductsResult>;

public sealed record GetProductsResult(IEnumerable<ProductDto> Products);

internal sealed class GetProductsHandler(CatalogDbContext dbContext) : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
        
        var productDtos = products.Adapt<List<ProductDto>>();
        
        return new GetProductsResult(productDtos);
    }
}