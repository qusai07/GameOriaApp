using Gameoria.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

//LoggingBehavior: لتسجيل العمليات


namespace Gameoria.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            var utcNow = _dateTime.UtcNow;

            _logger.LogInformation(
                "Gameoria Request: {RequestName} {@UserId} {@Request} {@UtcNow}",
                requestName, userId, request, utcNow);

            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();

            _logger.LogInformation(
                "Gameoria Response: {RequestName} {@UserId} took {ElapsedMilliseconds}ms {@Response}",
                requestName, userId, stopwatch.ElapsedMilliseconds, response);

            return response;
        }
    }
}
