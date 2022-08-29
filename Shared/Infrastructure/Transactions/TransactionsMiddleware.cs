using Microsoft.AspNetCore.Http;
using VirtualFreezer.Shared.Infrastructure.DAL;

namespace VirtualFreezer.Shared.Infrastructure.Transactions;

internal class TransactionsMiddleware : IMiddleware
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionsMiddleware(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
        => _unitOfWork.ExecuteAsync(() => next(context));
}