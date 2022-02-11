namespace Notes.Persistence;

public class NotesDbContex : DbContext, INotesDbContext
{
    public DbSet<Note> Notes { get; set; }

    public NotesDbContex(DbContextOptions<NotesDbContex> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new NoteConfiguration());
        base.OnModelCreating(builder);
    }
}
