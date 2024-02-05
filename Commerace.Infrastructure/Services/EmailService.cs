using Media.Application;
using Media.Application.Abstractions.Services;
using Media.Application.Dto;
using Media.Domain.Identity;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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





            smtpClient.Send(mail);

        }
    }
}
