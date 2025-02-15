using Domain.Exceptions;

namespace Domain.ValueObjects;

public record Location
{
    public string City { get; } = default!;
    public string Street { get; } = default!;

    private Location(string city, string street)
    {
        City = city;
        Street = street;
    }

    public static Location Of(string city, string street)
    {
        ArgumentException.ThrowIfNullOrEmpty(city);
        ArgumentException.ThrowIfNullOrEmpty(street);

        return new Location(city, street);
    }
}