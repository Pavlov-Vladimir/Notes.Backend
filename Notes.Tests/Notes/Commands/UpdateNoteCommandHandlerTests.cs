namespace Notes.Tests.Notes.Commands;
public class UpdateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateNoteCommandHandler_Success()
    {
        //Arrange
        UpdateNoteCommandHandler handler = new(Context);
        string newTitle = "newer title";
        //Act
        await handler.Handle(new UpdateNoteCommand
        {
            Id = NotesContextFactory.NoteIdForUpdate,
            UserId = NotesContextFactory.UserBId,
            Title = newTitle,
        },
        CancellationToken.None);
        //Assert
        Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
            note.Id == NotesContextFactory.NoteIdForUpdate && note.Title == newTitle));
    }

    [Fact]
    public async Task UpdateNoteCommandHandler_FailOnWrongId()
    {
        //Arrange
        UpdateNoteCommandHandler handler = new(Context);
        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new UpdateNoteCommand
            {
                Id = Guid.NewGuid(),
                UserId = NotesContextFactory.UserBId
            },
            CancellationToken.None));
    }

    [Fact]
    public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
    {
        //Arrange
        UpdateNoteCommandHandler handler = new(Context);
        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new UpdateNoteCommand
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                UserId = NotesContextFactory.UserAId
            },
            CancellationToken.None));
    }
}
