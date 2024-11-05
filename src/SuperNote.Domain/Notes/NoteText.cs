using FluentResults;

namespace SuperNote.Domain.Notes;

public record NoteText
{
    public string Value { get; }
    private NoteText(string value) => Value = value;

    public static Result<NoteText> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? 
            NoteErrors.TheNoteIsEmpty : 
            Result.Ok(new NoteText(value));
    }
}