namespace Application.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string message) 
        : base($"Пользователь {message} не найден")
    {

    }
}