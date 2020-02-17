using Projeto.API.ViewModels;
using Projeto.Business.Models;

namespace Projeto.API.Configuration
{
    public class AutomapperConfig : AutoMapper.Profile
    {
        public AutomapperConfig()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Profile, ProfileViewModel>().ReverseMap();
        }
    }
}
