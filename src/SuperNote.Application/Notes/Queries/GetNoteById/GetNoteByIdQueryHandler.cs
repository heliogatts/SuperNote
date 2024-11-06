using FluentResults;
using Optional;
using Optional.Unsafe;
using SuperNote.Application.Abstractions.Queries;
using SuperNote.Domain.Notes;

namespace SuperNote.Application.Notes.Queries.GetNoteById;

public class GetNoteByIdQueryHandler : IQueryHandler<GetNoteByIdQuery, Result<NoteResponse>>
{
    private readonly INotesRepository _noteRepository;

    public GetNoteByIdQueryHandler(INotesRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Result<NoteResponse>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        Option<Note> noteOption = await _noteRepository.GetByIdAsync(request.Id);
        
        if (!noteOption.HasValue)
        {
            return NoteErrors.NoteNotFound;
        }

        var note = noteOption.ValueOrDefault();
        NoteResponse noteResponse = new(note.Id.Value, note.NoteText.Value, note.LastModified);

        return Result.Ok(noteResponse);
    }
}