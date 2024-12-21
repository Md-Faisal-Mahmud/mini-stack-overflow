using Autofac;
using AutoMapper;
using MiniStackOverflow.Application.Features.Training.Services;
using MiniStackOverflow.Domain.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MiniStackOverflow.Web.Models
{
    public class ProfileEditModel
    {
        private ILifetimeScope _scope;
        private IProfileManagementService _profileManagementService;
        private IMapper _mapper;

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserName required")]
        public string UserName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }


        public ProfileEditModel() { }

        public ProfileEditModel(IProfileManagementService profileManagementService,
            IMapper mapper)
        {
            _profileManagementService = profileManagementService;
            _mapper = mapper;

        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _profileManagementService = _scope.Resolve<IProfileManagementService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public async Task EditProfileAsync()
        {
            await _profileManagementService.EditProfileAsync(UserId, UserName, Title,
                Description, Country, ImageUrl);
        }

        public async Task LoadAsync(Guid id)
        {
            User user = await _profileManagementService.GetProfileAsync(id);
            if (user != null)
            {
                _mapper.Map(user, this);
            }
        }

        internal bool IsImageFile(string fileName)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".svg" };
            string fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }

    }
}
