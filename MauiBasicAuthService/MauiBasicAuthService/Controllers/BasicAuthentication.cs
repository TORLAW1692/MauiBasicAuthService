using Microsoft.AspNetCore.Mvc.Filters;

namespace MauiBasicAuthService.Controllers
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Make sure an Authorization header was supplied.
            if (string.IsNullOrEmpty(
                context.HttpContext.Request.Headers.Authorization))
            {
                context.Result =
                    new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            }
            else
            {
                // Read the Authorization header.
                var authHeader =
                    context.HttpContext.Request.Headers.Authorization.ToString();

                var authHeaderParts = authHeader.Split(' ');

                // Header must be: Basic <base64 credentials>
                if (authHeaderParts.Length != 2 ||
                    authHeaderParts[0] != "Basic")
                {
                    context.Result =
                        new Microsoft.AspNetCore.Mvc.UnauthorizedResult();

                    return;
                }

                // Decode username:password.
                var credentials =
                    System.Text.Encoding.UTF8.GetString(
                        System.Convert.FromBase64String(authHeaderParts[1]));

                var parts = credentials.Split(':');

                // Validate the credentials.
                if (parts.Length != 2 ||
                    parts[0].ToLower() != "instructor01" ||
                    parts[1] != "Password01")
                {
                    context.Result =
                        new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                }
                else
                {
                    // Credentials are valid, so allow the request through.
                    base.OnActionExecuting(context);
                }
            }
        }
    }
}
