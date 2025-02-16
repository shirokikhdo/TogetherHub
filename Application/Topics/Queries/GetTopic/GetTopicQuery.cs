﻿namespace Application.Topics.Queries.GetTopic;

public record GetTopicQuery(Guid Id, CancellationToken CancellationToken)
    : IQuery<GetTopicResult>;