using MovieList.Domain.DTO.Email;

namespace MovieList.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage message);
        Task SendEmailAsync(EmailMessage message);
    }
}
