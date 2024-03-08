using Media.Application.Dtos;

namespace Media.Application.Services
{
     public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}