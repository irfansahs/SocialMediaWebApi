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
    public class UpdatePasswordCommand : IRequest<object>
    {
        public string resetToken { get; set; }
        public string userId { get; set; }
        public string newPassword { get; set; }


        public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, object>
        {

            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;
            readonly SignInManager<Media.Domain.Identity.AppUser> _signInManager;

            private readonly IMapper _mapper;
            private readonly IEmailService _emailService;
            readonly ITokenHandler _tokenHandler;

            public UpdatePasswordCommandHandler(IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager, SignInManager<Media.Domain.Identity.AppUser> signInManager, ITokenHandler tokenHandler, IEmailService emailService)
            {
                _mapper = mapper;
                _userManager = userManager;
                _tokenHandler = tokenHandler;
                _signInManager = signInManager;
                _emailService = emailService;
            }

            public async Task<object> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
            {

                Media.Domain.Identity.AppUser appUser = await _userManager.FindByIdAsync(request.userId);

                if (appUser != null)
                {

                    byte[] tokenBytes = WebEncoders.Base64UrlDecode(request.resetToken);
                    var resetToken = Encoding.UTF8.GetString(tokenBytes);

                   var result = await _userManager.ResetPasswordAsync(appUser, resetToken, request.newPassword);

                    return result;
                }


                return appUser;
            }
        }
    }
}

