using Application.Features.Users.Create;
using Application.Features.Users.GetByUserName;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserResponse>();

            CreateMap<User, GetUserByUserNameResponse>();
        }
    }
}
