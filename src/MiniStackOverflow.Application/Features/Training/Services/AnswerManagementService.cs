using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Application.Features.Training.Services
{
    public class AnswerManagementService : IAnswerManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public AnswerManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAnswerAsync(string answerText, Guid questionId, Guid userId, string userName)
        {
            Answer answer = new Answer()
            {
                AnswerText = answerText,
                QuestionId = questionId,
                UserId = userId,
                UserName = userName
            };
            await _unitOfWork.AnswerRepository.AddAsync(answer);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IList<Answer>> GetAnswersByQuestionIdAsync(Guid questionId)
        {
            return await _unitOfWork.AnswerRepository.GetByQuestionIdAsync(questionId);
        }
    }
}
