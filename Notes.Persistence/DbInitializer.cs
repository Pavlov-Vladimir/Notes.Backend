namespace Notes.Persistence;

public class DbInitializer
{
    public static void Initialize(NotesDbContex contex)
    {
        contex.Database.EnsureCreated();
    }
}
