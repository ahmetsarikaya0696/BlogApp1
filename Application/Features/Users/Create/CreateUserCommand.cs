using MediatR;

namespace Application.Features.Users.Create
{
    public record CreateUserCommand(string FirstName, string LastName, string UserName, string Email, string Password) : IRequest<ServiceResult<CreateUserResponse>>;
}
