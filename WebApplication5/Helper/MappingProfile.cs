using AutoMapper;
using WebApplication5.Model;
using WebApplication5.Model.Dto;

namespace WebApplication5.Helper
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() 
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserGetDto>();
        }
    }
}
