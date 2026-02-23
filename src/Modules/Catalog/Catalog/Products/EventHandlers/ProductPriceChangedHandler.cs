using Catalog.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Products.EventHandlers;

internal sealed class ProductPriceChangedHandler(ILogger<ProductPriceChangedHandler> logger) : INotificationHandler<ProductPriceChangedEvent>
{
    public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        // Todo: Update Basket Prices
        
        logger.LogInformation("Handled event {DomainEvent}", notification.GetType().Name);
        
        return Task.CompletedTask;
    }
}