using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuthors.Middlewares
{

    public static class ResponseLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseLogMiddleware>();
        }
    }

    public class ResponseLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLogMiddleware> _logger;

        public ResponseLogMiddleware(RequestDelegate next, ILogger<ResponseLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using var ms = new MemoryStream();
            var responseOriginalBody = context.Response.Body;
            context.Response.Body = ms;

            await _next(context);

            ms.Seek(0, SeekOrigin.Begin);
            string response = new StreamReader(ms).ReadToEnd();
            ms.Seek(0, SeekOrigin.Begin);

            await ms.CopyToAsync(responseOriginalBody);
            context.Response.Body = responseOriginalBody;

            _logger.LogInformation(response);
        }
    }
}
