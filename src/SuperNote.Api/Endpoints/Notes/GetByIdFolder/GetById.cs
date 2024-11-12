using FastEndpoints;
using MediatR;
using SuperNote.Api.Extensions;
using SuperNote.Application.Notes.Queries;
using SuperNote.Application.Notes.Queries.GetNoteById;
using SuperNote.Domain.Notes;

namespace SuperNote.Api.Endpoints.Notes.GetByIdFolder;

public class GetById(IMediator mediator) : Endpoint<GetNoteByIdRequest, NoteResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/notes/{noteId:guid}");
        
        Summary(s =>
        {
            s.Summary = "Retrieve a note by its ID";
        });
    }

    public override async Task HandleAsync(GetNoteByIdRequest req, CancellationToken ct)
    {
        var note = await mediator.Send(new GetNoteByIdQuery(new NoteId(req.NoteId)), ct);

        if (note.IsSuccess)
        {
            await SendOkAsync(note.Value, ct);
        }
        else
        {
            await this.SendProblemDetailsResponse(note, ct);
        }
    }
}