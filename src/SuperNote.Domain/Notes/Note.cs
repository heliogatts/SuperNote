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
    private Note()
    {
    }
    
    public Note(NoteId id, NoteText noteText, DateTime lastModified)
    {
        Id = id;
        NoteText = noteText;
        LastModified = lastModified;
        
        RaiseDomainEvent(new NoteCreatedDomainEvent(Id));;
    }
    
}