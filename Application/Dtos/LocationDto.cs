namespace Application.Dtos;

/// <summary>
/// DTO (Data Transfer Object) для представления местоположения.
/// </summary>
/// <param name="City">Название города.</param>
/// <param name="Street">Название улицы.</param>
public record LocationDto(
    string City,
    string Street);