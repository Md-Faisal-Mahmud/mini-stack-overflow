using Autofac;
using MiniStackOverflow.Application.Features.Training.Services;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MiniStackOverflow.Web.Models
{
    public class AnswerCreateModel
    {
        private ILifetimeScope _scope;
        private IAnswerManagementService _answerManagementService;
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid QuestionId { get; set; }

        [Required]
        public string AnswerText { get; set; }


        public AnswerCreateModel() { }

        public AnswerCreateModel(IAnswerManagementService answerManagementService)
        {
            _answerManagementService = answerManagementService;

        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _answerManagementService = _scope.Resolve<IAnswerManagementService>();
        }

        public async Task CreateAnswerAsync()
        {
            await _answerManagementService.CreateAnswerAsync(AnswerText, QuestionId, UserId, UserName);
        }
    }
}
