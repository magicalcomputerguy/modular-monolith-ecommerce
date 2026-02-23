using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Products.Features.GetProductsByCategory;

public sealed record GetProductsByCategoryQuery(string Category) : IRequest<GetProductsByCategoryResult>;

public sealed record GetProductsByCategoryResult(IEnumerable<ProductDto> Products);

internal sealed class GetProductsByCategoryHandler(CatalogDbContext dbContext) : IRequestHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .Where(p => p.Category.Contains(request.Category))
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();
        
        return new GetProductsByCategoryResult(productDtos);
    }
}