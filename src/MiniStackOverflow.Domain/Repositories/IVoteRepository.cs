using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Domain.Repositories
{
    public interface IVoteRepository : IRepositoryBase<Vote, Guid>
    {
        Task<IList<Vote>> GetByQuestionIdAsync(Guid questionId);
        Task<IList<Vote>> GetByQuestionIdAndUserIdAsync(Guid questionId, Guid userId);
    }
}
