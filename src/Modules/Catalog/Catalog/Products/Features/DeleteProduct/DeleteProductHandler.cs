using Catalog.Data;
using MediatR;

namespace Catalog.Products.Features.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResult>;

public sealed record DeleteProductResult(bool Success);

internal sealed class DeleteProductHandler(CatalogDbContext dbContext) : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([request.Id], cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product not found with id {request.Id}");
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new DeleteProductResult(true);
    }
}