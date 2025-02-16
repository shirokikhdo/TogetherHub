namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateTopicHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UpdateTopicResult> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var dto = request.RequestTopicDto;

        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics.FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        topic.Update(
            dto.Title, 
            dto.Summary, 
            dto.TopicType,
            dto.EventStart, 
            dto.Location.City,
            dto.Location.Street);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateTopicResult(topic.ToResponseTopicDto());
    }
}