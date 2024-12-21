using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Application.Features.Training.Services
{
    public interface ICommentManagementService
    {
        Task CreateCommentAsync(string commentText, Guid UserId, Guid questionId, string userName);

        Task<IList<Comment>> GetCommentsByQuestionIdAsync(Guid questionId);
    }
}