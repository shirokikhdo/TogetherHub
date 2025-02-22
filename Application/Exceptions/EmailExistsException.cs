namespace Application.Exceptions;

public class EmailExistsException : UserExistsException
{
    public EmailExistsException(string email) 
        : base($"Пользователь с Email {email} уже существует")
    {
        
    }
}