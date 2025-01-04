using Application.Dtos;

namespace Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        void SendEmail(EmailDto emailDto);
    }
}
