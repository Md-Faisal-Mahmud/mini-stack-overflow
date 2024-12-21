using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Domain.Repositories
{
    public interface IAnswerRepository : IRepositoryBase<Answer, Guid>
    {
        Task<IList<Answer>> GetByQuestionIdAsync(Guid questionId);
        Task<int> GetAnswerCountByUserIdAsync(Guid userId);
    }
}
