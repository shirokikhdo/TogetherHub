namespace Application.Topics.Commands.JoinLeaveTopic;

public class JoinLeaveTopicHandler 
    : ICommandHandler<JoinLeaveTopicCommand, JoinLeaveTopicResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;

    public JoinLeaveTopicHandler(
        IApplicationDbContext dbContext,
        IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
    }

    public async Task<JoinLeaveTopicResult> Handle(JoinLeaveTopicCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}