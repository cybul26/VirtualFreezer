namespace VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

internal record ValidationError(string Code, string Message, object Errors);