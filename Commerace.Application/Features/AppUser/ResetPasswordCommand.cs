using AutoMapper;
using Commerace.Application.Abstractions;
using Commerace.Application.Features.AppUser;
using Media.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MimeKit.Text;

namespace Media.Application.Features.AppUser
{
    public class ResetPasswordCommand : IRequest<object>
    {
        public string Email { get; set; }

        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, object>
        {

            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;
            readonly SignInManager<Media.Domain.Identity.AppUser> _signInManager;

            private readonly IMapper _mapper;
            private readonly IEmailService _emailService;
            readonly ITokenHandler _tokenHandler;

            public ResetPasswordCommandHandler(IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager, SignInManager<Media.Domain.Identity.AppUser> signInManager, ITokenHandler tokenHandler, IEmailService emailService)
            {
                _mapper = mapper;
                _userManager = userManager;
                _tokenHandler = tokenHandler;
                _signInManager = signInManager;
                _emailService = emailService;
            }

            public async Task<object> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {

                Media.Domain.Identity.AppUser appUser = await _userManager.FindByEmailAsync(request.Email);

                // token oluştur bu token ile kullanıcıyı aktif yap

                if (appUser != null)
                {

                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    byte[] bytes = Encoding.UTF8.GetBytes(resetToken);
                    resetToken = WebEncoders.Base64UrlEncode(bytes);


                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("sosyalmedya531@gmail.com", "Sosyal medya projesi");
                    mail.To.Add(appUser.Email);
                    mail.Subject = "Sosyal Medya projesi sifre yenileme";
                    mail.IsBodyHtml = true;


                    string text = "<a href='http://localhost:3000/PasswordReset/" + appUser.Id + "/" + resetToken + "> Sifre Yenileme </a>";
                    mail.Body = text;

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("sosyalmedya531@gmail.com", "lgedmmkkamkkfibb"),
                        EnableSsl = true,
                    };

                   smtpClient.Send(mail);

                    return appUser.Id+" "+resetToken;
                }


                return appUser;
            }
        }
    }
}

