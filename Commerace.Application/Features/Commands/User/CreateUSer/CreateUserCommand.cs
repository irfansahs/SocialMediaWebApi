using AutoMapper;
using Commerace.Application.Dto;
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

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewDto>
        {

            private readonly IMapper _mapper;
            readonly UserManager<Domain.Identity.AppUser> _userManager;

            public CreateUserCommandHandler(IMapper mapper, UserManager<Domain.Identity.AppUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<UserViewDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                IdentityResult result = await _userManager.CreateAsync(new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = request.UserName,
                    Email = request.Email,

                }, request.Password);

                UserViewDto response = new() { Succeeded = result.Succeeded };

                return response;
            }
        }
    }
}
