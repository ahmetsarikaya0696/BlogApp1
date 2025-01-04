using Application.Features.Posts.Create;
using Application.Features.Posts.Update;
using AutoMapper;
using Domain.Entities;
using MassTransit;

namespace Application.Features.Posts
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostCommand, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => NewId.NextSequentialGuid()));

            CreateMap<UpdatePostCommand, Post>();
        }
    }
}
