using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Media.Application.Features.User.Commands
{
     public class UpdateUserCommand : IRequest<object>
    {
        public string UserName { get; set; }
        public string? UserColor { get; set; }
        public string? Email { get; set; }
      //  public string? ProfileImage { get; set; }
        public string? PhoneNumber { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, object>
        {

            private readonly IMapper _mapper;
            readonly UserManager<Media.Domain.Entities.Identity.AppUser> _userManager;


            public UpdateUserCommandHandler(IMapper mapper, UserManager<Media.Domain.Entities.Identity.AppUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;

            }

            public async Task<object> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByNameAsync(request.UserName);

                if (request.PhoneNumber != null)
                {
                    user.PhoneNumber = request.PhoneNumber;
                }

                if (request.UserColor != null)
                {
                    user.UserColor = request.UserColor;
                }

                if (request.Email != null)
                {
                    user.Email = request.Email;
                }
/*
                if (request.ProfileImage != null)
                {
                    user.ProfileImage = request.ProfileImage;
                }
*/
                var result = await _userManager.UpdateAsync(user);

                return result;
            }
        }

    }
}