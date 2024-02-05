using AutoMapper;
using Media.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.AppUser
{
    public class RefreshTokenLoginCommand : IRequest<object>
    {
        public string RefreshToken { get; set; }

        public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommand, object>
        {

            private readonly IMapper _mapper;
            readonly UserManager<Domain.Identity.AppUser> _userManager;
            readonly SignInManager<Domain.Identity.AppUser> _signInManager;
            public readonly ITokenHandler _tokenHandler;
            readonly IUserService _userService;


            public RefreshTokenLoginCommandHandler(ITokenHandler tokenHandler, IMapper mapper, SignInManager<Domain.Identity.AppUser> signInManager, UserManager<Domain.Identity.AppUser> userManager, IUserService userService)
            {
                _mapper = mapper;
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenHandler = tokenHandler;
                _userService = userService;
            }

            public async Task<object> Handle(RefreshTokenLoginCommand request, CancellationToken cancellationToken)
            {
                var appuser = await _userManager.Users.FirstOrDefaultAsync(u => u.RefhreshToken == request.RefreshToken);

                if (appuser != null && appuser?.RefhreshTokenEndDate > DateTime.UtcNow)
                {
                    var token = _tokenHandler.CreateAccessToken(5);
                    await _userService.UpdateRefreshToken(token.RefreshToken, appuser.Id, token.Expiration, 5);

                    var response = new
                    {
                        Id = appuser.Id,
                        Token = token,
                        profileImage = appuser.ProfileImage,
                        userName = appuser.UserName,
                        userColor = appuser.UserColor,
                    };
                    return response;
                }
                return new Exception("User Not Found");
            }
        }
    }
}
