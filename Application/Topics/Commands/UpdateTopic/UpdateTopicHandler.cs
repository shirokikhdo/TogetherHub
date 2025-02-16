namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateTopicHandler(
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UpdateTopicResult> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var dto = request.RequestTopicDto;

        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics.FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        _mapper.Map(dto, topic);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateTopicResult(topic.ToResponseTopicDto());
    }
}