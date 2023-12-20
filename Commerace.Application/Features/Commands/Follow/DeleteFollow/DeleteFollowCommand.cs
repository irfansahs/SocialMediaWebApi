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
        public string UserName { get; set; }
        public string FollowTo { get; set; }


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
                

                var followDelete = await _repository.DeleteFollow(request.UserName, request.FollowTo);

                return followDelete;
            }


        }

    }
}
