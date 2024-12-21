using Autofac;
using MiniStackOverflow.Application.Features.Training.Services;

namespace MiniStackOverflow.Application;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<QuestionManagementService>().As<IQuestionManagementService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AnswerManagementService>().As<IAnswerManagementService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TagManagementService>().As<ITagManagementService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ProfileManagementService>().As<IProfileManagementService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<VoteManagementService>().As<IVoteManagementService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CommentManagementService>().As<ICommentManagementService>()
            .InstancePerLifetimeScope();
    }
}
