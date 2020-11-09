using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace Shop.Api.Infrastructure.Filters
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger logger;

        public LoggingBehaviour(ILogger logger)
        {
            this.logger = logger?.ForContext<ExceptionFilter>();
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            // Request
            logger.Information($"Handling {typeof(TRequest).Name}");
            var myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (var prop in props)
            {
                var propValue = prop.GetValue(request, null);
                logger.Information("{Property} : {@Value}", prop.Name, propValue);
            }

            var response = await next();

            // Response
            logger.Information($"Handled {typeof(TResponse).Name}");

            return response;
        }
    }
}