using AutoMapper;
using Commerace.Application.Dto;
using Media.Application.Dto;
using Media.Domain;
using Media.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.User.CreateUSer
{
    public class CreateUserCommand : IRequest<UserViewDto>
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public string UserColor { get; set; }


        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewDto>
        {

            private readonly IMapper _mapper;
            private readonly IEmailService _emailService;

            readonly UserManager<Domain.Identity.AppUser> _userManager;

            public CreateUserCommandHandler(IMapper mapper, UserManager<Domain.Identity.AppUser> userManager, IEmailService emailService)
            {
                _mapper = mapper;
                _userManager = userManager;
                _emailService = emailService;
            }

            public async Task<UserViewDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                Random random = new Random();
                int kod = random.Next(100000, 1000000);

                IdentityResult result = await _userManager.CreateAsync(new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = request.UserName,
                    Email = request.Email,
                    ProfileImage = request.ProfileImage,
                    UserColor = request.UserColor,
                    ConfirmCode = kod,

                }, request.Password); ;

                if (result.Succeeded)
                {

                    EmailDto email = new EmailDto
                    {
                        To = request.Email,
                        Subject = "Email Confirm Code",
                        Body = kod.ToString(),
                    };

                    _emailService.SendEmail(email);
                }

                UserViewDto response = new() { Succeeded = result.Succeeded };

                return response;
            }
        }
    }
}
