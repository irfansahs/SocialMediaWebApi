﻿using AutoMapper;
using Commerace.Application.Dto;
using Media.Application.Abstractions.Services;
using Media.Domain;
using Media.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommand : IRequest<object>
    {

        public string UserName { get; set; }
        public string Password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, object>
        {

            private readonly IMapper _mapper;
            readonly UserManager<Domain.Identity.AppUser> _userManager;
            readonly SignInManager<Domain.Identity.AppUser> _signInManager;
            public readonly ITokenHandler _tokenHandler;
            readonly IUserService _userService;


            public LoginUserCommandHandler(ITokenHandler tokenHandler, IMapper mapper, SignInManager<Domain.Identity.AppUser> signInManager, UserManager<Domain.Identity.AppUser> userManager, IUserService userService)
            {
                _mapper = mapper;
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenHandler = tokenHandler;
                _userService = userService;
            }

            public async Task<object> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var appuser = await _userManager.FindByNameAsync(request.UserName);
                if (appuser == null)
                {
                    throw new Exception("Kullanıcı bulundamadı");
                }
                var result = await _signInManager.CheckPasswordSignInAsync(appuser, request.Password, false);
                if (result.Succeeded)
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
                return result;
            }
        }

    }
}
