using AutoMapper;
using Google.Apis.Auth;
using Media.Application.Features.User.Dtos;
using Media.Application.Services.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Media.Application.Features.User.Commands
{
    public class GoogleLoginCommand : IRequest<object>
    {
        public string IdToken { get; set; }
        public string Provider { get; set; }
        public string Email { get; set; }

        public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, object>
        {

            readonly UserManager<Media.Domain.Entities.Identity.AppUser> _userManager;
            readonly SignInManager<Media.Domain.Entities.Identity.AppUser> _signInManager;
            readonly IUserService _userService;


            private readonly IMapper _mapper;
            readonly ITokenHandler _tokenHandler;


            public GoogleLoginCommandHandler(IUserService userService, IMapper mapper, UserManager<Media.Domain.Entities.Identity.AppUser> userManager, SignInManager<Media.Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
            {
                _mapper = mapper;
                _userManager = userManager;
                _tokenHandler = tokenHandler;
                _signInManager = signInManager;
                _userService = userService;
            }


            public async Task<object> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
            {


                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { "359247775554-f776m6md9juksvfh4anfccfmsroc32o0.apps.googleusercontent.com" },
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

                var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);


                Media.Domain.Entities.Identity.AppUser appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (appUser == null)
                {
                    appUser = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,


                    };
                    await _userManager.CreateAsync(appUser);
                }

                await _userManager.AddLoginAsync(appUser, info);

                Token token = _tokenHandler.CreateAccessToken(5);

                await _userService.UpdateRefreshToken(token.RefreshToken, appUser.Id, token.Expiration, 5);

                return token;
            }
        }
    }
}