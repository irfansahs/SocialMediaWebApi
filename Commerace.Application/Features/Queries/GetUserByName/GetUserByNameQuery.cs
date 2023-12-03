using AutoMapper;
using Commerace.Application.Dto;
using Media.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllProducts
{
    public class GetUserByNameQuery : IRequest<UserResponseDto>
    {
        public string UserName { get; set; }
        public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, UserResponseDto>
        {

            private readonly IMapper _mapper;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;


            public GetUserByNameQueryHandler(IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }


            public async Task<UserResponseDto> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
            {
                var user = _userManager.Users.Where(x=>x.UserName==request.UserName).FirstOrDefault();

                UserResponseDto viewmodel = _mapper.Map<UserResponseDto>(user);

                return viewmodel;
            }
        }
    }
}
