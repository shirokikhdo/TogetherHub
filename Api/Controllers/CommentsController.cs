namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{topicId:guid}")]
    public async Task<IResult> CreateComment(
        Guid topicId,
        [FromBody] CreateCommentDto createCommentDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateCommentCommand(topicId, createCommentDto.Text);
        var result = await _mediator.Send(command, cancellationToken);
        return Results.Ok(result.Comment);
    }

    [HttpGet("{topicId:guid}")]
    public async Task<IResult> CreateComment(
        Guid topicId,
        CancellationToken cancellationToken)
    {
        var command = new GetCommentsQuery(topicId);
        var result = await _mediator.Send(command, cancellationToken);
        return Results.Ok(result.Comments);
    }
}