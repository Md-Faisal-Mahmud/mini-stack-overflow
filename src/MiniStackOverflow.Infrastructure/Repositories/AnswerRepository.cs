
using Microsoft.EntityFrameworkCore;
using MiniStackOverflow.Domain.Entities;
using MiniStackOverflow.Domain.Repositories;
using System.Linq.Expressions;

namespace MiniStackOverflow.Infrastructure.Repositories
{
    public class AnswerRepository : Repository<Answer, Guid>, IAnswerRepository
    {
        public AnswerRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<IList<Answer>> GetByQuestionIdAsync(Guid questionId)
        {
            Expression<Func<Answer, bool>> filter = a => a.QuestionId == questionId;
            var answers = await GetAsync(filter, null);
            return answers;
        }

        public async Task<int> GetAnswerCountByUserIdAsync(Guid userId)
        {
            Expression<Func<Answer, bool>> filter = q => q.UserId == userId;
            int count = await GetCountAsync(filter);
            return count;
        }

    }
}
