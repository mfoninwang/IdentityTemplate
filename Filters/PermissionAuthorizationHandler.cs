﻿using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Filters
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler() { }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null) { return;}

            var permissions = context.User.Claims.Where(x => x.Type == "Permission" &&
                x.Value == requirement.Permission &&                                                                
                x.Issuer == "LOCAL AUTHORITY");

            if (permissions.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}