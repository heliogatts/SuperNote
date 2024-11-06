using MediatR;
using Microsoft.Extensions.Logging;
using SuperNote.Domain.Notes;

namespace SuperNote.Application.Notes.Events;

public sealed class NoteCreatedDomainEventHandler : INotificationHandler<NoteCreatedDomainEvent>
{
    private readonly ILogger<NoteCreatedDomainEvent> _logger;
    
    public NoteCreatedDomainEventHandler(ILogger<NoteCreatedDomainEvent> logger)
    {
        _logger = logger;
    }
    
    public async Task Handle(NoteCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Note created, Id: {Id}", notification.NoteId);
        await Task.CompletedTask;
    }
}