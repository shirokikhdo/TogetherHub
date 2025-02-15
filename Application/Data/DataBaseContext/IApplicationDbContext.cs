namespace Application.Data.DataBaseContext;

public interface IApplicationDbContext
{
    public DbSet<Topic> Topics { get; }
}