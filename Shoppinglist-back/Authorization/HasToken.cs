using Microsoft.AspNetCore.Authorization;

namespace Shoppinglist_back.Authorization;

public class HasToken : IAuthorizationRequirement
{
    public HasToken(Boolean token)
    {
        hasToken = token;
    }
    public Boolean hasToken { get; set; }
}
