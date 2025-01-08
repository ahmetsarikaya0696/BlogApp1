using Application.Features.Tags.Create;
using Application.Features.Tags.Update;
using AutoMapper;
using Domain.Entities;
using MassTransit;

namespace Application.Features.Tags
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<CreateTagCommand, Tag>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => NewId.NextSequentialGuid()));

            CreateMap<UpdateTagCommand, Tag>();
        }
    }
}
