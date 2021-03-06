using System.Net;

namespace VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

internal class ExceptionResponse
{
    public object Response { get; }
    public HttpStatusCode StatusCode { get; }

    public ExceptionResponse(object response, HttpStatusCode statusCode)
    {
        Response = response;
        StatusCode = statusCode;
    }
}