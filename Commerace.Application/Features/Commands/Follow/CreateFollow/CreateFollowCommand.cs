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
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }

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
                    FollowerId = request.FollowerId,
                    FollowingId = request.FollowingId,
                    CreatedOn = DateTime.UtcNow,
                };

                await _repository.AddAsync(Follow);

                return null;
            }
        }
    }
}
