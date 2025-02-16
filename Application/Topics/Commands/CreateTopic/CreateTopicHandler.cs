namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateTopicHandler(
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var topic = _mapper.Map<Topic>(request.RequestTopicDto);

        await _dbContext.Topics.AddAsync(topic, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTopicResult(topic.ToResponseTopicDto());
    }
}