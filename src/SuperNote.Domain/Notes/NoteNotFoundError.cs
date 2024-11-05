using SuperNote.Domain.Abstractions.ErrorHandling;

namespace SuperNote.Domain.Notes;

public class NoteNotFoundError : DomainError
{
    public NoteNotFoundError(string message, string code) 
        : base(message, code)
    {
        WithMetadata(nameof(ErrorType), ErrorType.NotFound);
    }
}