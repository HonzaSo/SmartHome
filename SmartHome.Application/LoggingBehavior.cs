using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SmartHome.Application;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var requestName = typeof(TRequest).Name;
        var timer = Stopwatch.StartNew();

        using (logger.BeginScope(new Dictionary<string, object> 
               { 
                   ["RequestName"] = requestName 
               }))
        {
            logger.LogInformation("[START] Handling {RequestName}", requestName);

            try
            {
                var response = await next();

                timer.Stop();
                logger.LogInformation("[END] Handled {RequestName} in {Elapsed}ms", 
                    requestName, timer.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                timer.Stop();
                logger.LogError(ex, "[ERROR] {RequestName} failed after {Elapsed}ms. Message: {ErrorMessage}", 
                    requestName, timer.ElapsedMilliseconds, ex.Message);

                throw; 
            }
        }
    }
}