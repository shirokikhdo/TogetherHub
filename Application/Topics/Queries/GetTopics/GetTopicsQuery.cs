using Shared.CQRS;

namespace Application.Topics.Queries.GetTopics;

public record GetTopicsQuery(CancellationToken CancellationToken) 
    : IQuery<GetTopicsResult>;