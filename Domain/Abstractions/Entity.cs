namespace Domain.Abstractions;

/// <summary>
/// Абстрактный класс, представляющий сущность с идентификатором и статусом удаления.
/// </summary>
/// <typeparam name="T">Тип идентификатора сущности.</typeparam>
public abstract class Entity<T> : IEntity<T>
{
    /// <summary>
    /// Получает или устанавливает идентификатор сущности.
    /// </summary>
    public required T Id { get; set; }

    /// <summary>
    /// Получает или устанавливает значение, указывающее, была ли сущность удалена.
    /// </summary>
    public bool IsDeleted { get; set; } = default!;

    /// <summary>
    /// Получает или устанавливает дату и время удаления сущности.
    /// Если сущность не удалена, значение будет равно null.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; } = default!;
}