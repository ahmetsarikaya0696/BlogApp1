using MediatR;

namespace Application.Features.Tags.GetAll
{
    public record GetAllTagsQuery() : IRequest<ServiceResult<List<TagDto>>>;
}
