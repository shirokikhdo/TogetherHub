using Domain.Enums;

namespace Infrastructure.Security.Auth;

public class TopicDeletionRequirementHandler 
    : AuthorizationHandler<TopicDeletionRequirement>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TopicDeletionRequirementHandler(
        IApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        TopicDeletionRequirement requirement)
    {
        var userId = context.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            context.Fail();
            return;
        }

        var routeValue = _httpContextAccessor
            .HttpContext?
            .Request
            .RouteValues;

        var value = routeValue?
            .FirstOrDefault(x => x.Key == "id")
            .Value?
            .ToString();

        if (string.IsNullOrEmpty(value))
        {
            context.Fail();
            return;
        }

        var topicId = TopicId.Of(Guid.Parse(value));

        var relationship = await _dbContext.Relationships
            .AsNoTracking()
            .FirstOrDefaultAsync(x => 
                x.UserReference == userId.ToString() 
                && x.TopicReference == topicId);

        if (relationship?.Role == ParticipantRole.Organizer)
            context.Succeed(requirement);
    }
}