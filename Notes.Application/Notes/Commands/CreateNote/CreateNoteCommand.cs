namespace Notes.Application.Notes.Commands.CreateNote;
public class CreateNoteCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Details { get; set; } = null!;
}
