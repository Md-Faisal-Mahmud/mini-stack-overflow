﻿using Microsoft.AspNetCore.Identity;
using System;

namespace MiniStackOverflow.Infrastructure.Membership
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
            : base()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
        }
    }
}
