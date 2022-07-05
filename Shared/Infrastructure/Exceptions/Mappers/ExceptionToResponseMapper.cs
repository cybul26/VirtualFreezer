using System.Collections.Concurrent;
using System.Net;
using FluentValidation;
using Humanizer;
using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Shared.Infrastructure.Exceptions.Mappers;

internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    private static readonly ConcurrentDictionary<Type, string> Codes = new();

    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            CustomException ex => new ExceptionResponse(new Error(GetErrorCode(ex), ex.Message)
                , HttpStatusCode.BadRequest),
            ValidationException ex => new ExceptionResponse(new ValidationError("validation",
                "One or more validation errors occured!",
                GroupToDictionary(
                    ex.Errors.ToList(), f => char.ToLowerInvariant(f.PropertyName[0]) + f.PropertyName[1..],
                    v => v.ErrorMessage)), HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new Error("error", "There was an error."),
                HttpStatusCode.InternalServerError)
        };


    private static string GetErrorCode(object exception)
    {
        var type = exception.GetType();
        return Codes.GetOrAdd(type, type.Name.Underscore().Replace("_exception", string.Empty));
    }

    private static Dictionary<TKey, List<TValue>> GroupToDictionary<TItem, TKey, TValue>(List<TItem> items,
        Func<TItem, TKey> keySelector, Func<TItem, TValue> valueSelector)
        where TKey : notnull
    {
        var dict = new Dictionary<TKey, List<TValue>>();

        foreach (var item in items)
        {
            var key = keySelector(item);
            if (!dict.TryGetValue(key, out var group))
            {
                group = new List<TValue>(1);
                dict.Add(key, group);
            }

            group.Add(valueSelector(item));
        }

        return dict;
    }
}