namespace Domain.Abstractions;

/// <summary>
/// Интерфейс, представляющий базовую сущность.
/// </summary>
public interface IEntity
{
    
}

/// <summary>
/// Интерфейс, представляющий сущность с идентификатором.
/// </summary>
/// <typeparam name="T">Тип идентификатора сущности.</typeparam>
public interface IEntity<T> : IEntity
{
    /// <summary>
    /// Получает или устанавливает идентификатор сущности.
    /// </summary>
    public T Id { get; set; }
}