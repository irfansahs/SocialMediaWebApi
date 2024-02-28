using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;

namespace Media.Application.Features.Likes.Commands
{
    public class DeleteLikeCommand : IRequest<object>
    {
        public int PostId { get; set; }
        public string UserId { get; set; }

        public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, object>
        {

            private readonly ILikeRepository _repository;
            private readonly IMapper _mapper;


            public DeleteLikeCommandHandler(ILikeRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
            {

                var like = _repository.AsQueryable()
                           .FirstOrDefault(i => i.PostId == request.PostId && i.UserId == request.UserId);

                await _repository.DeleteAsync(like);

                return null;
            }


        }

    }
}