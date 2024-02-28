using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;

namespace Media.Application.Features.Likes.Commands
{
    public class CreateLikeCommand : IRequest<object>
    {
        public int PostId { get; set; }
        public string UserId { get; set; }

        public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, object>
        {

            private readonly ILikeRepository _repository;
            private readonly IMapper _mapper;


            public CreateLikeCommandHandler(ILikeRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
            {

                var Like = new Media.Domain.Entities.Like
                {
                    UserId = request.UserId,
                    PostId = request.PostId,
                    CreatedOn = DateTime.UtcNow,
                };

                await _repository.AddAsync(Like);

                return null;
            }


        }

    }
}