using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Media.Application.Features.User.Commands
{
    public class EmailConfirmCommand : IRequest<object>
    {
        public string Email { get; set; }
        public int kod { get; set; }

        public class EmailConfirmCommandHandler : IRequestHandler<EmailConfirmCommand, object>
        {
            private readonly IMapper _mapper;
            private readonly UserManager<Media.Domain.Entities.Identity.AppUser> _userManager;

            public EmailConfirmCommandHandler(IMapper mapper, UserManager<Media.Domain.Entities.Identity.AppUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<object> Handle(EmailConfirmCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);


                if (user.ConfirmCode == request.kod)
                {

                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);

                    return "Email Confirmed";
                }

                return null;

            }
        }
    }
}