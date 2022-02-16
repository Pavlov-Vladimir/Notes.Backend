namespace Notes.Tests.Notes.Commands;
public class CreateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateNoteCommandHandler_Success()
    {
        //Arrange
        CreateNoteCommandHandler? handler = new(Context);
        string noteName = "note name";
        string noteDetails = "note details";
        //Act
        var noteId = await handler.Handle(new CreateNoteCommand
        {
            Title = noteName,
            Details = noteDetails,
            UserId = NotesContextFactory.UserAId
        },
            CancellationToken.None);
        //Assert
        Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
            note.Id == noteId && note.Title == noteName && note.Details == noteDetails));
    }


}
