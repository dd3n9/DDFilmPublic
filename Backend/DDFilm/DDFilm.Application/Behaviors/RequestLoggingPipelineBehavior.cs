using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;


namespace DDFilm.Application.Behaviors
{
    internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
        where TResponse : ResultBase
    {
        private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogInformation("Processing request {RequestName}", requestName);
            
            TResponse result = await next();

            //if (cancellationToken.IsCancellationRequested)
            //    _logger.LogInformation("Request { RequestName} was cancelled", requestName);

            if(result.IsSuccess)
            {
                _logger.LogInformation("Completed request {RequestName}", requestName);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Errors, true))
                {
                    _logger.LogError("Completed request {RequestName} with error", requestName);
                }
            }

            return result;
        }
    }
}
