using Microsoft.AspNetCore.Builder;
using WebApiAuthors.Middlewares;

namespace WebApiAuthors.Extensions
{
    public static class ResponseLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseLogMiddleware>();
        }
    }
}
