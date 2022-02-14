namespace Notes.Application.Notes.Commands.UpdateNote;
public class DeleteNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(updateNoteCommand => updateNoteCommand.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(updateNoteCommand => updateNoteCommand.Id)
             .NotEqual(Guid.Empty);
        RuleFor(updateNoteCommand => updateNoteCommand.Title)
            .NotEmpty()
            .MaximumLength(250);
    }
}
