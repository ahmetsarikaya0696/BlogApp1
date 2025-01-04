using Application.Contracts.Infrastructure;
using Application.Dtos;
using Application.Events;
using Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Consumers
{
    public class UserCreatedEventConsumer(IEmailService emailService, UserManager<User> userManager) : IConsumer<UserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var user = (await userManager.FindByIdAsync(context.Message.UserId))!;

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user!);

            var confirmationLink = $"https://localhost:7293/api/email/confirm?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            // E-posta gönderimi
            var body = $@"<p>Merhaba {user.FirstName} {user.LastName},</p>
                          <p>Lütfen e-posta adresinizi doğrulamak için aşağıdaki bağlantıya tıklayın:</p>
                          <p><a href='{confirmationLink}'>E-posta Doğrulama</a></p>
                          <p>Teşekkürler,</p>
                          <p>Ahmet Sarıkaya</p>";

            emailService.SendEmail(new EmailDto(user!.Email!, "Doğrulama E-postası", body));
        }
    }
}
