using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.Features.Emails.Confirm
{
    public class ConfirmEmailCommandHandler(UserManager<User> userManager) : IRequestHandler<ConfirmEmailCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user is null) return ServiceResult.Error("Kullanıcı bulunamadı.", HttpStatusCode.NotFound);

            var result = await userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded) return ServiceResult.Error("E-posta doğrulama işlemi başarısız.", HttpStatusCode.BadRequest);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
