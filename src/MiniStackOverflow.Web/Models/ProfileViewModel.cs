using Autofac;
using AutoMapper;
using MiniStackOverflow.Application.Features.Training.Services;
using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Web.Models
{
    public class ProfileViewModel
    {
        private ILifetimeScope _scope;
        private IProfileManagementService _profileManagementService;
        private IMapper _mapper;

        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? ImageUrl { get; set; }
        public Stream FileStream { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? Questions { get; set; }
        public int? Answers { get; set; }


        public ProfileViewModel() { }

        public ProfileViewModel(IProfileManagementService profileManagementService,
            IMapper mapper)
        {
            _profileManagementService = profileManagementService;
            _mapper = mapper;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _profileManagementService = _scope.Resolve<IProfileManagementService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public async Task ViewProfileAsync(Guid id)
        {
            User user = await _profileManagementService.GetProfileAsync(id);
            if (user != null)
            {
                _mapper.Map(user, this);
            }
        }

        public async Task<User> ViewProfileAsyncByUserNameAsync(string userName)
        {
            return await _profileManagementService.GetProfileByUserNameAsync(userName);
        }

        public async Task CreateUserAsync(Guid userId, string userName)
        {
            User user = await _profileManagementService.GetProfileAsync(userId);
            if (user == null)
            {
                await _profileManagementService.EditProfileAsync(userId, userName, Title,
                    Description, Country, ImageUrl);
            }
        }
    }
}
