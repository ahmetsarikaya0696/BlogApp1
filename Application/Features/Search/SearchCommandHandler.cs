using Application.Contracts.Infrastructure;
using MediatR;

namespace Application.Features.Search
{
    public record SearchCommandHandler(IPostElasticsearchRepository postElasticsearchRepository) : IRequestHandler<SearchCommand, ServiceResult<SearchResponse>>
    {
        public async Task<ServiceResult<SearchResponse>> Handle(SearchCommand request, CancellationToken cancellationToken)
        {
            var (postList, totalCount) = await postElasticsearchRepository.SearchAsync(request.PostSearchDto, request.Page, request.PageSize);

            var pageLinkCount = (totalCount / request.PageSize);
            if (totalCount % request.PageSize != 0) pageLinkCount += 1;

            var response = new SearchResponse(postList, totalCount, pageLinkCount);

            return ServiceResult<SearchResponse>.SuccessAsOk(response);
        }
    }
}
