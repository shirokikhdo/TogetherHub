using Application.Dtos;

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
}