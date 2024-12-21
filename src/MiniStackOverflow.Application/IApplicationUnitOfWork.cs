using MiniStackOverflow.Domain;
using MiniStackOverflow.Domain.Repositories;

namespace MiniStackOverflow.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        ICommentRepository CommentRepository { get; }
        ITagRepository TagRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IVoteRepository VoteRepository { get; }
    }
}
