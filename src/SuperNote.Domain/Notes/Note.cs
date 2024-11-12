using SuperNote.Domain.Abstractions.Aggregates;
using SuperNote.Domain.Abstractions.DomainEvents;

namespace SuperNote.Domain.Notes;

public class Note : AggregateRoot
{
    public NoteId Id { get; }
    public NoteText NoteText { get; }
    public DateTime LastModified { get; }

    /// <summary>
    /// EfCore demands a parameterless constructor
    /// </summary>
    public Note()
    {
    }
    
    public Note(NoteText noteText, DateTime lastModified)
    {
        Id = new NoteId(Guid.NewGuid());
        NoteText = noteText;
        LastModified = lastModified;
        
        RaiseDomainEvent(new NoteCreatedDomainEvent(Id));;
    }
    
}