using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Features.Tags.Update
{
    public class UpdateTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateTagCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.GetByIdAsync(request.Id);

            if (tag is null) return ServiceResult.Error("Belirtilen ID'ye sahip tag bulunamadı", HttpStatusCode.NotFound);

            mapper.Map(request, tag);

            tagRepository.Update(tag);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
