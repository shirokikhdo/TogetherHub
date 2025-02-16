namespace Domain.Models;

public class Topic : Entity<TopicId>
{
    public string Title { get; set; } = default!;
    public DateTime? EventStart { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string TopicType { get; set; } = default!;
    public Location Location { get; set; } = default!;

    public static Topic Create(
        TopicId topicId,
        string title,
        DateTime eventStart,
        string summary,
        string topicType,
        Location location)
    {
        ArgumentException.ThrowIfNullOrEmpty(title);
        ArgumentException.ThrowIfNullOrEmpty(summary);
        ArgumentException.ThrowIfNullOrEmpty(topicType);

        var topic = new Topic
        {
            Id = topicId,
            Title = title,
            EventStart = eventStart,
            Summary = summary,
            TopicType = topicType,
            Location = location
        };
        
        return topic;
    }

    public void Update(
        string title, 
        string summary, 
        string topicType,
        DateTime eventStart,
        string city,
        string street)
    {
        if (!string.IsNullOrEmpty(title))
            Title = title;

        if (!string.IsNullOrEmpty(summary))
            Summary = summary;

        if (!string.IsNullOrEmpty(topicType))
            TopicType = topicType;

        Location = Location.Of(
            string.IsNullOrEmpty(city)
            ? Location.City
            : city,
            string.IsNullOrEmpty(street)
                ? Location.Street
                : street);

        EventStart = eventStart;
    }
}