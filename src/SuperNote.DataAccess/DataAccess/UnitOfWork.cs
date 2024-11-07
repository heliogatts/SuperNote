using MediatR;
using SuperNote.Domain.Abstractions.Aggregates;
using SuperNote.Domain.Abstractions.DataAccess;
using SuperNote.Domain.Abstractions.DomainEvents;

namespace SuperNote.DataAccess.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly IPublisher _publisher;
    private readonly SuperNoteContext _context;

    public UnitOfWork(IPublisher publisher, SuperNoteContext context)
    {
        _publisher = publisher;
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = GetDomainEntities(_context);
        var domainEvents = GetDomainEvents(domainEntities);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        // dispatching domain events
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
        
        domainEntities.ForEach(entity => entity.ClearDomainEvents());
        
        List<AggregateRoot> GetDomainEntities(SuperNoteContext context) => 
            context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(entry => entry.Entity)
                .ToList();
        
        List<IDomainEvent> GetDomainEvents(List<AggregateRoot> entities) =>
            entities
                .Where(entity => entity.DomainEvents.Any())
                .SelectMany(entity => entity.DomainEvents)
                .ToList();
    }
}