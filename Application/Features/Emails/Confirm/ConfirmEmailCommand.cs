using MediatR;

namespace Application.Features.Emails.Confirm
{
    public record ConfirmEmailCommand(string UserId, string Token) : IRequest<ServiceResult>;
}
