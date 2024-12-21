using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Application.Features.Training.Services
{
    public class CommentManagementService : ICommentManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public CommentManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCommentAsync(string commentText, Guid userId, Guid questionId, string userName)
        {
            Comment comment = new Comment()
            {
                CommentText = commentText,
                UserId = userId,
                QuestionId = questionId,
                UserName = userName
            };
            await _unitOfWork.CommentRepository.AddAsync(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IList<Comment>> GetCommentsByQuestionIdAsync(Guid questionId)
        {
            return await _unitOfWork.CommentRepository.GetByQuestionIdAsync(questionId);
        }
    }
}
