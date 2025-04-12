namespace Domain.Exceptions;

public class CommentException : DomainException
{
    public CommentException(string message) 
        : base(message)
    {

    }

    public CommentException(Guid id, string text)
        : base($"Проблема с комментарием: {text}. TopicId: ({id})")
    {

    }
}