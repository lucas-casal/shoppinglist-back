using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Shoppinglist_back.Authorization
{
    public class TokenAuthorization : AuthorizationHandler<HasToken>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasToken requirement)
        {
            var usernameClaim = context.User.FindFirst(claim => claim.Type == "id");

            if (usernameClaim == null) { return  Task.CompletedTask; }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
