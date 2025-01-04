using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Features.Tags.Create
{
    public class CreateTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateTagCommand, ServiceResult<CreateTagResponse>>
    {
        public async Task<ServiceResult<CreateTagResponse>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var sameTagExists = await tagRepository.AnyAsync(x => x.Name == request.Name);

            if (sameTagExists) return ServiceResult<CreateTagResponse>.Error("Aynı isimli tag tekrar eklenemez", HttpStatusCode.BadRequest);

            var mappedTag = mapper.Map<Tag>(request);

            await tagRepository.AddAsync(mappedTag);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateTagResponse>.SuccessAsCreated(new CreateTagResponse(mappedTag.Id), $"api/tags/{mappedTag.Id}");
        }
    }
}
