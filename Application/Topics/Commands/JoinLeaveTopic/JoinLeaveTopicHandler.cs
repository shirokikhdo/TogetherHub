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

    public async Task<JoinLeaveTopicResult> Handle(
        JoinLeaveTopicCommand request, 
        CancellationToken cancellationToken)
    {
        var topic = await GetTopicAsync(request.Id, cancellationToken);
        var currentUser = await GetCurrentUserAsync(cancellationToken);

        var organizer = topic.Users
            .FirstOrDefault(x => x.Role == ParticipantRole.Organizer)?
            .CurrentUser;

        if (organizer is not null
            && organizer.UserName == currentUser.UserName)
            return await ToggleTopicStatusAsync(topic, cancellationToken);
        
        return await UpdateCurrentUserStatusAsync(topic, currentUser, cancellationToken);
    }

    private async Task<Topic> GetTopicAsync(Guid id, CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(id);

        var topic = await _dbContext.Topics
            .Include(x => x.Users)
            .ThenInclude(x => x.CurrentUser)
            .FirstOrDefaultAsync(x => x.Id == topicId, cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        return topic;
    }

    private async Task<CustomIdentityUser> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var username = _userAccessor.GetUsername();
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);

        if (user is null)
            throw new UserNotFoundException(username);

        return user;
    }

    private async Task<JoinLeaveTopicResult> ToggleTopicStatusAsync(
        Topic topic, 
        CancellationToken cancellationToken)
    {
        var oldStatus = topic.IsVoided;
        topic.IsVoided = !oldStatus;

        _dbContext.Topics.Update(topic);
        var success = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return new JoinLeaveTopicResult(
            $"Статус изменился: {oldStatus} -> {topic.IsVoided}",
            success); ;
    }

    private async Task<JoinLeaveTopicResult> UpdateCurrentUserStatusAsync(
        Topic topic, 
        CustomIdentityUser currentUser, 
        CancellationToken cancellationToken)
    {
        var joinUser = topic.Users
            .FirstOrDefault(x => x.CurrentUser.UserName == currentUser.UserName);

        var detail = string.Empty;

        if (joinUser is null)
        {
            var relationship = Relationship.Create(
                RelationshipId.Of(Guid.NewGuid()),
                currentUser.Id,
                currentUser,
                ParticipantRole.Participant,
                topic.Id,
                topic);

            topic.Users.Add(relationship);
            detail = $"Вы присоединились ({topic.Id.Value})";
        }
        else
        {
            topic.Users.Remove(joinUser);
            detail = $"Вы покинули ({topic.Id.Value})";
        }

        _dbContext.Topics.Update(topic);
        var success = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return new JoinLeaveTopicResult(detail, success);
    }
}