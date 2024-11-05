namespace SuperNote.Domain.Notes;

public record struct NoteId(Guid Value)
{
    public static NoteId New() => new(Guid.NewGuid());
}