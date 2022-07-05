namespace VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

public abstract class CustomException : Exception
{
    public CustomException(string message) : base(message)
    {
    }
}