using AutoMapper;
using GameOria.Shared.DTOs.SigUp;
using GameOria.Shared.ViewModels;

namespace GameOria.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SignupViewModel, SignupParameters>();
            CreateMap<SignupParameters, SignupViewModel>();


        }
    }
}
