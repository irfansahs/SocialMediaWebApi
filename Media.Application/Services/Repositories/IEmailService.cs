using Media.Application.Dtos;

namespace Media.Application.Services.Repositories
{
     public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}