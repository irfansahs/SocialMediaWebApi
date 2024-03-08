using System.Net;
using System.Net.Mail;
using Media.Application.Dtos;
using Media.Application.Services;

namespace Media.Infrastructure.Services
{
    public class EmailService : IEmailService
    {

        public void SendEmail(EmailDto request)
        {
             // "sosyalmedya531@gmail.com", "lgedmmkkamkkfibb"


            MailMessage mail = new MailMessage();


            mail.From = new MailAddress("sosyalmedya531@gmail.com", "Sosyal medya projesi");
            mail.To.Add(request.To);
            mail.Subject = request.Subject;
         //   mail.IsBodyHtml = true;


            string text = request.Body;

            mail.Body = text;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sosyalmedya531@gmail.com", "lgedmmkkamkkfibb"),
                EnableSsl = true,
            };

            smtpClient.Send(mail);

        }

    }
}