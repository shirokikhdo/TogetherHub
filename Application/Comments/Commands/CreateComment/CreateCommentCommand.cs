namespace Application.Comments.Commands.CreateComment;

public record CreateCommentCommand(
        Guid TopicId, 
        string Text)
    : ICommand<CreateCommentResult>;