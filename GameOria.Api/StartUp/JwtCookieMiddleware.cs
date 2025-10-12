using GameOria.Infrastructure.Helper.Service;

namespace GameOria.Api.StartUp
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var jwtHelper = scope.ServiceProvider.GetRequiredService<JwtHelper>();

                var token = context.Request.Cookies["AuthToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    var principal = jwtHelper.ValidateTokenAndGetPrincipal(token);
                    if (principal != null)
                    {
                        context.User = principal;
                    }
                }
            }

            await _next(context);
        }
    }
}
