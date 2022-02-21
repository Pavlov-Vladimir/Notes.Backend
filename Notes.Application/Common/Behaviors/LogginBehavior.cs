﻿namespace Notes.Application.Common.Behaviors;
public class LogginBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;

    public LogginBehavior(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        string requestName = typeof(TRequest).Name;
        Guid userId = _currentUserService.UserId;

        Log.Information("Notes Request: {Name} {@UserId}, {@Request}", requestName, userId, request);

        var response = await next();
        return response;
    }
}
