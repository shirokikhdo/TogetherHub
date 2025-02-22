﻿namespace Application.Exceptions;

public class UserExistsException : Exception
{
    public UserExistsException(string message)
        : base(message)
    {
        
    }
}