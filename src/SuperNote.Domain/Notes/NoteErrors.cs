using SuperNote.Domain.Abstractions.ErrorHandling;

namespace SuperNote.Domain.Notes;

public static class NoteErrors
{
    public static readonly NoteNotFoundError NoteNotFound = new("note.not.found", "Note not found");
    public static readonly NoteTextIsEmpty TheNoteIsEmpty = new("note.text.is.empty", "Note text is empty");
}