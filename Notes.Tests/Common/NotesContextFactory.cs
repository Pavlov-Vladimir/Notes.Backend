namespace Notes.Tests.Common;
public class NotesContextFactory
{
    public static Guid UserAId = Guid.NewGuid();
    public static Guid UserBId = Guid.NewGuid();

    public static Guid NoteIdForDelete = Guid.NewGuid();
    public static Guid NoteIdForUpdate = Guid.NewGuid();

    public static NotesDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new NotesDbContext(options);
        context.Database.EnsureCreated();
        context.Notes.AddRange(
            new Note
            {
                UserId = UserAId,
                Id = Guid.Parse("30D49ED3-780E-44F6-B30D-27538F16650D"),
                Title = "Title1",
                Details = "Details1",
                CreationDate = DateTime.Today,
                EditDate = null
            },
            new Note
            {
                UserId = UserBId,
                Id = Guid.Parse("99AA28B9-38B4-43E3-A90A-09DC68C8E983"),
                Title = "Title2",
                Details = "Details2",
                CreationDate = DateTime.Today,
                EditDate = null
            },
            new Note
            {
                UserId = UserAId,
                Id = NoteIdForDelete,
                Title = "Title3",
                Details = "Details3",
                CreationDate = DateTime.Today,
                EditDate = null
            },
            new Note
            {
                UserId = UserBId,
                Id = NoteIdForUpdate,
                Title = "Title4",
                Details = "Details4",
                CreationDate = DateTime.Today,
                EditDate = null
            }
        );
        context.SaveChanges();
        return context;
    }

    public static void Destroy(NotesDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
