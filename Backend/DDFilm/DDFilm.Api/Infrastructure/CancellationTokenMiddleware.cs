using MediatR;

namespace DDFilm.Api.Infrastructure
{
    public class CancellationLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<CancellationLoggingBehavior<TRequest, TResponse>> _logger;

        public CancellationLoggingBehavior(ILogger<CancellationLoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = await next();
                return response;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                _logger.LogWarning("Request {Request} was cancelled.", typeof(TRequest).Name);
                throw; 
            }
        }
    }
}
