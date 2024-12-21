using AutoMapper;
using MiniStackOverflow.Domain.Entities;
using MiniStackOverflow.Web.Models;

namespace StackOverflow.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<ProfileViewModel, User>()
                .ReverseMap();

            CreateMap<ProfileEditModel, User>()
                .ReverseMap();

            CreateMap<QuestionDetailsModel, Question>()
                .ReverseMap();
        }
    }
}
