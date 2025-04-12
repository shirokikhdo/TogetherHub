namespace Application.Exceptions;

public class OrganizerException : Exception
{
    public OrganizerException(string message)
        : base(message)
    {
        
    }

    public OrganizerException(string username, object key)
        : base($"У пользователя {username} не прав ({key})")
    {

    }
}