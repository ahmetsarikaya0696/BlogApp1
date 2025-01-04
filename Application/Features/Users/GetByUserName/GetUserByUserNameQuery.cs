using MediatR;

namespace Application.Features.Users.GetByUserName
{
    public record GetUserByUserNameQuery(string UserName) : IRequest<ServiceResult<GetUserByUserNameResponse>>;
}
