using System;

using Microsoft.AspNetCore.Identity;

namespace MiniStackOverflow.Infrastructure.Membership
{
    public class ApplicationUserToken
        : IdentityUserToken<Guid>
    {

    }
}
