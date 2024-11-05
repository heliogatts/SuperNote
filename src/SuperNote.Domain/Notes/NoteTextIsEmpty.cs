using SuperNote.Domain.Abstractions.ErrorHandling;

namespace SuperNote.Domain.Notes;

public class NoteTextIsEmpty : DomainError
{
    public NoteTextIsEmpty(string message, string code) 
        : base(message, code)
    {
        WithMetadata(nameof(ErrorType), ErrorType.InvalidData);
    }
}