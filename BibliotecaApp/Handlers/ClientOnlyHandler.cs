using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BibliotecaApp.Handlers
{
    public class ClientOnlyHandler : AuthorizationHandler<ClientOnlyRequirement>
    {
        private readonly ILogger<ClientOnlyHandler> _logger;

        public ClientOnlyHandler(ILogger<ClientOnlyHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientOnlyRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
                var userName = isAuthenticated ? httpContext.User.Identity.Name : "Anonimo";
                var userRoles = isAuthenticated ? string.Join(", ", httpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(c => c.Value)) : "Nenhuma";

                if(isAuthenticated && httpContext.User.IsInRole("Cliente"))
                {
                    _logger.LogInformation(message: $"Acesso autorizado para o usuário {userName} com as roles: {userRoles}");
                    context.Succeed(requirement);
                } else
                {
                    _logger.LogWarning(message: $"Acesso negado para o usuário {userName} com as roles: {userRoles}");
                    context.Fail();
                }
            } else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
