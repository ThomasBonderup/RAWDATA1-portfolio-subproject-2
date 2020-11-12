using AutoMapper;
using DataAccess;

namespace WebService.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile ()
        {
            CreateMap<Name, NameListDto>().ReverseMap();
            CreateMap<Title, TitleListDto>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();
            CreateMap<TitlePrincipals, TitlePrincipalsDto>().ReverseMap();
        }
    }
}