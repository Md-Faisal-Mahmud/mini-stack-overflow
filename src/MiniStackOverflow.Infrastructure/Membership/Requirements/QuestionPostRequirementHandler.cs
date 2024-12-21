using Microsoft.AspNetCore.Authorization;

namespace MiniStackOverflow.Infrastructure.Membership.Requirements
{
    public class QuestionPostRequirementHandler :
          AuthorizationHandler<QuestionPostRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               QuestionPostRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "PostQuestion" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
