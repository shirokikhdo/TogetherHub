namespace Application.Dtos;

public record ResponseTopicDto(
    Guid Id,
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime? EventStart);