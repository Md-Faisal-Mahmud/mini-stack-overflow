using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Domain.Repositories
{
    public interface ICommentRepository : IRepositoryBase<Comment, Guid>
    {
        Task<IList<Comment>> GetByQuestionIdAsync(Guid questionId);
    }
}
