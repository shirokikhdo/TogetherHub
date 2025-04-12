namespace Application.Comments.Queries.GetComments;

public record GetCommentsQuery(Guid TopicId)
    : IQuery<GetCommentsResult>;