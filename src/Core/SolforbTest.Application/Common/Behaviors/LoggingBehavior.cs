using MediatR;
using Serilog;

namespace SolforbTest.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            string requestName = typeof(TRequest).Name;

            Log.Information("System request start: {Name} {@Request}", requestName, request);

            var response = await next();

            Log.Information("System request end: {Name} {@Request}", requestName, request);

            return response;
        }
    }
}
