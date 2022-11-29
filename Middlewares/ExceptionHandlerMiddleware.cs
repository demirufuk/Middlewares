using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        public RequestDelegate next { get; }
        public ILogger<ExceptionHandlerMiddleware> logger { get; }

        public ExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> Logger)
        {
            this.next = Next;
            logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);

            }
            catch (Exception ex)
            {
                //exception handler
                logger.LogError(ex.Message);
            }
        }

    }
}
