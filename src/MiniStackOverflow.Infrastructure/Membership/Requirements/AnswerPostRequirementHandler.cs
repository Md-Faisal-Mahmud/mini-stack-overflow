using Microsoft.AspNetCore.Authorization;

namespace MiniStackOverflow.Infrastructure.Membership.Requirements
{
    public class AnswerPostRequirementHandler :
          AuthorizationHandler<AnswerPostRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               AnswerPostRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "PostAnswer" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
