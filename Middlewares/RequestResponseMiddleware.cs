using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Text;

namespace Middlewares
{
    public class RequestResponseMiddleware
    {
        public RequestDelegate next { get; }
        public ILogger<RequestResponseMiddleware> logger;

        public RequestResponseMiddleware(RequestDelegate Next, ILogger<RequestResponseMiddleware> Logger)
        {
            this.next = Next;
            logger = Logger;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            //Get Request
            var orginalBodyStream = httpContext.Response.Body;

            logger.LogInformation($"Query Keys: {httpContext.Request.QueryString}");

            MemoryStream requestBody = new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0, SeekOrigin.Begin);
            String requestText = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0, SeekOrigin.Begin);


            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream;

            await next.Invoke(httpContext); //Response


            //Get Response Body
            tempStream.Seek(0, SeekOrigin.Begin);
            String responseText = await new StreamReader(tempStream, Encoding.UTF8).ReadToEndAsync();
            tempStream.Seek(0, SeekOrigin.Begin);

            await httpContext.Response.Body.CopyToAsync(orginalBodyStream);

            logger.LogInformation($"Request:  {requestText}");
            logger.LogInformation($"Response: {responseText}");


        }
    }
}
