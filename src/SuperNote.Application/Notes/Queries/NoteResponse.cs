namespace SuperNote.Application.Notes.Queries;

public record NoteResponse(
    Guid Id, 
    string Text, 
    DateTime LastModified);