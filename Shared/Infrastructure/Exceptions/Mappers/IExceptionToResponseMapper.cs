using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Shared.Infrastructure.Exceptions.Mappers;

internal interface IExceptionToResponseMapper
{
    ExceptionResponse Map(Exception exception);
}