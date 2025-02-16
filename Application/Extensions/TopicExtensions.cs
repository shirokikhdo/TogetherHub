using Application.Dtos;
using Domain.ValueObjects;

namespace Application.Extensions;

public static class TopicExtensions
{
    public static ResponseTopicDto ToResponseTopicDto(this Topic topic) =>
        new ResponseTopicDto(
            topic.Id.Value,
            topic.Title,
            topic.Summary,
            topic.TopicType,
            new LocationDto(topic.Location.City, topic.Location.Street),
            topic.EventStart);

    public static List<ResponseTopicDto> ToResponseTopicDtoList(this List<Topic> topics) =>
        topics.Select(x=>x.ToResponseTopicDto()).ToList();

    public static Topic ToTopic(this CreateTopicDto dto, TopicId id) =>
        new Topic
        {
            Id = id,
            Title = dto.Title,
            Summary = dto.Summary,
            TopicType = dto.TopicType,
            Location = Location.Of(dto.Location.City, dto.Location.Street),
            EventStart = dto.EventStart,
        };
}