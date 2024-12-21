using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Application.Features.Training.Services
{
    public interface IAnswerManagementService
    {
        Task CreateAnswerAsync(string answerText, Guid questionId, Guid userId, string userName);

        Task<IList<Answer>> GetAnswersByQuestionIdAsync(Guid questionId);
    }
}