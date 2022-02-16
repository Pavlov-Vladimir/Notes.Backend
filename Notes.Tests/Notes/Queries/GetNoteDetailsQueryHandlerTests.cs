using Notes.Application.Notes.Queries.GetNoteDetails;

namespace Notes.Tests.Notes.Queries;

[Collection("QueryCollection")]
public class GetNoteDetailsQueryHandlerTests
{
    private readonly NotesDbContext Context;
    private readonly IMapper Mapper;

    public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetNoteDetailsQueryHandler_Success()
    {
        //Arrange
        GetNoteDetailsQueryHandler handler = new(Context, Mapper);
        //Act
        var result = await handler.Handle(
            new GetNoteDetailsQuery
            {
                UserId = NotesContextFactory.UserBId,
                Id = Guid.Parse("99AA28B9-38B4-43E3-A90A-09DC68C8E983")
            },
            CancellationToken.None);
        //Assert
        result.ShouldBeOfType<NoteDetailsVm>();
        result.Title.ShouldBe("Title2");
        result.CreationDate.ShouldBe(DateTime.Today);
    }
}
