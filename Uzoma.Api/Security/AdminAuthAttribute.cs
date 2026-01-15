using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;

namespace Uzoma.Api.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AdminAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var config = context.HttpContext.RequestServices
                .GetService(typeof(IConfiguration)) as IConfiguration;

            var expectedUsername = config?["AdminAuth:Username"];
            var expectedPassword = config?["AdminAuth:Password"];

            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Basic "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var encoded = authHeader.Substring("Basic ".Length).Trim();
                var decodedBytes = Convert.FromBase64String(encoded);
                var decoded = Encoding.UTF8.GetString(decodedBytes);

                var parts = decoded.Split(':');
                if (parts.Length != 2)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var username = parts[0];
                var password = parts[1];

                if (username != expectedUsername || password != expectedPassword)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
