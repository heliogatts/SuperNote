using Microsoft.AspNetCore.Mvc;

namespace SuperNote.Api.Endpoints.Notes.GetByIdFolder;

public record GetNoteByIdRequest
{
    [FromRoute]
    public Guid NoteId { get; set; }
}