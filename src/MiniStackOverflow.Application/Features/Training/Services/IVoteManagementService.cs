using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Application.Features.Training.Services
{
    public interface IVoteManagementService
    {
        Task CreateVoteAsync(Guid questionId, Guid userId, int voteType);
        Task<IList<Vote>> GetVotesByQuestionIdAsync(Guid questionId);
    }
}