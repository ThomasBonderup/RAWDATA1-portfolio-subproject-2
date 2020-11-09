using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebService.Middleware
{
    public static class AuthMiddlewareExtension
    {
        public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }

    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDataService _dataService;

        public AuthMiddleware(RequestDelegate next, IDataService dataService)
        {
            _next = next;
            _dataService = dataService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Program.CurrentUser = null;

            var auth = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(auth))
            {
                Program.CurrentUser = _dataService.GetUser((auth.ToString()));
            }

            await _next(context);
        }
    }
}