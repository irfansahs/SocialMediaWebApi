using AutoMapper;
using Commerace.Application.Dto;
using Media.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.User.DeleteUser
{
    public class DeleteUserCommand : IRequest<UserViewDto>
    {
        public int Id { get; set; }


        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserViewDto>
        {

            private readonly IMapper _mapper;

            public DeleteUserCommandHandler(IMapper mapper)
            {
                _mapper = mapper;
            }

            public async Task<UserViewDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {

                return null;
            }
        }
    }
}
