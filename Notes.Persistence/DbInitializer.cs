namespace Notes.Persistence;

public class DbInitializer
{
    public static void Initialize(NotesDbContext contex)
    {
        contex.Database.EnsureCreated();
    }
}
