using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Follow.CreateFollow
{
    public class CreateFollowCommand : IRequest<object>
    {
        public string UserName { get; set; }
        public string FollowTo { get; set; }

        public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, object>
        {

            private readonly IFollowRepository _repository;
            private readonly IMapper _mapper;


            public CreateFollowCommandHandler(IFollowRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
            {
                var Follow = new Media.Domain.Follow
                {
                    UserName = request.UserName,
                    FollowTo = request.FollowTo,
                };
                await _repository.AddAsync(Follow);

                return Follow;
            }
        }
    }
}
