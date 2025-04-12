namespace Application.Comments.Queries.GetComments;

public class GetCommentsHandler 
    : IQueryHandler<GetCommentsQuery, GetCommentsResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCommentsHandler(
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetCommentsResult> Handle(
        GetCommentsQuery request, 
        CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(request.TopicId);
        
        var сomments = await _dbContext.Comments
            .AsNoTracking()
            .Where(x=>x.CurrentTopic.Id == topicId)
            .ProjectTo<ResponseCommentDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x=>x.CreateAt)
            .ToListAsync(cancellationToken);

        return new GetCommentsResult(сomments);
    }
}