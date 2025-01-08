using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Events;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Features.Tags.Update
{
    public class UpdateTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork, IMapper mapper, IServiceBus serviceBus) : IRequestHandler<UpdateTagCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.GetByIdAsync(request.Id);

            if (tag is null) return ServiceResult.Error("Belirtilen ID'ye sahip tag bulunamadı", HttpStatusCode.NotFound);

            mapper.Map(request, tag);
            tagRepository.Update(tag);
            await unitOfWork.SaveChangesAsync();

            await serviceBus.PublishAsync(new TagUpdatedEvent(request.Id, request.Name), cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
