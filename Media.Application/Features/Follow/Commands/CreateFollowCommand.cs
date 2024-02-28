using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;

namespace Media.Application.Features.Follow.Commands
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
                
                var Follow = new Media.Domain.Entities.Follow
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