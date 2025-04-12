namespace Application.Topics.Dtos;

public record RelationshipDto(
    RelationshipId Id,
    TopicId TopicReference,
    string UserReference,
    ParticipantRole Role,
    ResponseTopicDto TopicDto,
    UserProfileDto UserDto);