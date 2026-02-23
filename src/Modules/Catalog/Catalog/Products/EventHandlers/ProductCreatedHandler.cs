using Catalog.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Products.EventHandlers;

internal sealed class ProductCreatedHandler(ILogger<ProductCreatedHandler> logger) : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handled event {DomainEvent}", notification.GetType().Name);
        
        return Task.CompletedTask;
    }
}