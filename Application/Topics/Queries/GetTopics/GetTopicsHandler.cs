namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler : IQueryHandler<GetTopicsQuery, GetTopicsResult>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTopicsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetTopicsResult> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
    {
        var topics = await _dbContext.Topics
            .AsNoTracking()
            .Where(x=>!x.IsDeleted)
            .ToListAsync(cancellationToken);

        var result = new GetTopicsResult(topics.ToResponseTopicDtoList());
        return result;
    }
}