using SyriacSources.Backend.Domain.Events;
using Microsoft.Extensions.Logging;

namespace SyriacSources.Backend.Application.TodoItems.EventHandlers;

public class RoleCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<RoleCreatedEventHandler> _logger;

    public RoleCreatedEventHandler(ILogger<RoleCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SyriacSources.Backend Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
