using Application.Contracts.Persistence;
using MediatR;
using System.Net;

namespace Application.Features.Tags.Delete
{
    public class DeleteTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTagCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.GetByIdAsync(request.Id);

            if (tag is null) return ServiceResult.Error("Belirtilen ID'ye sahip tag bulunamadı", HttpStatusCode.NotFound);

            tagRepository.Delete(tag);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
