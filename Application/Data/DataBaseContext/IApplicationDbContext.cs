namespace Application.Data.DataBaseContext;

/// <summary>
/// Интерфейс для контекста базы данных приложения.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Получает набор сущностей типа <see cref="Topic"/> для работы с темами.
    /// </summary>
    DbSet<Topic> Topics { get; }

    DbSet<Relationship> Relationships { get; }

    DbSet<Comment> Comments { get; }

    DbSet<CustomIdentityUser> Users { get; set; }

    /// <summary>
    /// Асинхронно сохраняет изменения в базе данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для прерывания операции, если это необходимо.</param>
    /// <returns>Задача, представляющая результат операции сохранения. Возвращает количество измененных строк.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}