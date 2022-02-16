namespace Notes.Tests.Notes.Commands;
public class DeleteNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteNoteCommandHandler_Success()
    {
        //Arrange
        DeleteNoteCommandHandler? handler = new(Context);
        //Act
        await handler.Handle(new DeleteNoteCommand
        {
            Id = NotesContextFactory.NoteIdForDelete,
            UserId = NotesContextFactory.UserAId
        },
        CancellationToken.None);
        //Assert
        Assert.Null(Context.Notes.SingleOrDefault(note =>
            note.Id == NotesContextFactory.NoteIdForDelete));
    }

    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongId()
    {
        //Arrange
        DeleteNoteCommandHandler? handler = new(Context);
        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new DeleteNoteCommand
            {
                Id = Guid.NewGuid(),
                UserId = NotesContextFactory.UserAId
            },
            CancellationToken.None));
    }

    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
    {
        //Arrange
        DeleteNoteCommandHandler? deleteHandler = new(Context);
        CreateNoteCommandHandler? createHandler = new(Context);
        Guid noteId = await createHandler.Handle(new CreateNoteCommand
        {
            Title = "NoteTitle",
            Details = "NoteDetails",
            UserId = NotesContextFactory.UserAId
        },
        CancellationToken.None);
        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await deleteHandler.Handle(new DeleteNoteCommand
            {
                Id = noteId,
                UserId = NotesContextFactory.UserBId
            },
            CancellationToken.None));
    }
}
