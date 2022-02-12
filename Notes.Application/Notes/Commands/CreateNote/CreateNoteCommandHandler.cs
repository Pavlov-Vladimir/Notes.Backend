namespace Notes.Application.Notes.Commands.CreateNote;
public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly INotesDbContext _dbContext;

    public CreateNoteCommandHandler(INotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        Note note = new()
        {
            UserId = request.UserId,
            Title = request.Title,
            Details = request.Details,
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
            EditDate = null
        };

        await _dbContext.Notes.AddAsync(note, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}
