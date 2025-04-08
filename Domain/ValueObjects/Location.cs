namespace Domain.ValueObjects;

/// <summary>
/// Представляет географическое местоположение с указанием города и улицы.
/// </summary>
public record Location
{
    /// <summary>
    /// Получает название города.
    /// </summary>
    public string City { get; } = default!;

    /// <summary>
    /// Получает название улицы.
    /// </summary>
    public string Street { get; } = default!;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Location"/> с заданными 
    /// значениями для города и улицы.
    /// </summary>
    /// <param name="city">Название города.</param>
    /// <param name="street">Название улицы.</param>
    private Location(string city, string street)
    {
        City = city;
        Street = street;
    }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="Location"/> с заданными 
    /// значениями для города и улицы.
    /// </summary>
    /// <param name="city">Название города.</param>
    /// <param name="street">Название улицы.</param>
    /// <returns>Созданный экземпляр <see cref="Location"/>.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если <paramref name="city"/> 
    /// или <paramref name="street"/> являются пустыми или равными null.</exception>
    public static Location Of(string city, string street)
    {
        ArgumentException.ThrowIfNullOrEmpty(city);
        ArgumentException.ThrowIfNullOrEmpty(street);

        return new Location(city, street);
    }
}