namespace Application.Data.DataBaseContext;

public interface IApplicationDbContext
{
    public DbSet<Topic> Topics { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}