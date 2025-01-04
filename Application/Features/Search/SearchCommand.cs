using Application.Dtos;
using MediatR;

namespace Application.Features.Search
{
    public record SearchCommand(PostSearchDto PostSearchDto, int Page, int PageSize) : IRequest<ServiceResult<SearchResponse>>;

}
