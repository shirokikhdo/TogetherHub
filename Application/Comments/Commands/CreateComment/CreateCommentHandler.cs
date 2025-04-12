namespace Application.Comments.Commands.CreateComment;

public class CreateCommentHandler 
    : ICommandHandler<CreateCommentCommand, CreateCommentResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public CreateCommentHandler(
        IApplicationDbContext dbContext, 
        IMapper mapper,
        IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<CreateCommentResult> Handle(
        CreateCommentCommand request, 
        CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(request.TopicId);
        var topic = await _dbContext.Topics
            .FindAsync([topicId], cancellationToken);

        if (topic is null 
            || topic.IsDeleted)
            throw new TopicNotFoundException(request.TopicId);

        var username = _userAccessor.GetUsername();
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);

        if (user is null)
            throw new UserNotFoundException(username);

        var comment = Comment.Create(
            CommentId.Of(Guid.NewGuid()),
            user,
            topic,
            DateTime.Now,
            request.Text);

        topic.Comments.Add(comment);

        var success = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        if (!success)
            throw new CreateCommentException(topic.Id.Value, request.Text);

        var result = _mapper.Map<ResponseCommentDto>(comment);

        return new CreateCommentResult(result);
    }
}