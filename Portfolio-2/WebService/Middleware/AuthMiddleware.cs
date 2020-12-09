using System.Threading.Tasks;
using AutoMapper.Configuration;
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
        private readonly IConfiguration _configuration;

        public AuthMiddleware(RequestDelegate next, IDataService dataService, IConfiguration configuration)
        {
            _next = next;
            _dataService = dataService;
            _configuration = configuration;
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