using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Follow.DeleteFollow
{
    public class DeleteFollowCommand : IRequest<object>
    {
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }

        public class DeleteFollowCommandHandler : IRequestHandler<DeleteFollowCommand, object>
        {

            private readonly IFollowRepository _repository;
            private readonly IMapper _mapper;


            public DeleteFollowCommandHandler(IFollowRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(DeleteFollowCommand request, CancellationToken cancellationToken)
            {
                var follow = _repository.AsQueryable()
                           .FirstOrDefault(i => i.FollowerId == request.FollowerId && i.FollowingId == request.FollowingId);

                await _repository.DeleteAsync(follow);

                return null;
            }


        }

    }
}
