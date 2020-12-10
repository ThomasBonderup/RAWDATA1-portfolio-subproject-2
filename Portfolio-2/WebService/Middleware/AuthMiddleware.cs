using System.Drawing;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AutoMapper.Configuration;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

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
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Auth:Secret").Value);

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == "uconst");
                if (claim != null)
                {
                    var uconst = claim.Value;
                    context.Items["User"] = _dataService.GetUser(uconst);
                }

            }
            catch
            {
            }
            await _next(context);
        }

        /* public async Task InvokeAsync(HttpContext context)
         {
             Program.CurrentUser = null;
 
             var auth = context.Request.Headers["Authorization"].ToString();
             if (!string.IsNullOrEmpty(auth))
             {
                 Program.CurrentUser = _dataService.GetUser((auth.ToString())); //det her er et problem fordi vi finder vores
                 //getUser ved uconst og ikke username ligesom Henriks
             }
 
             await _next(context);
         }*/
    }
}