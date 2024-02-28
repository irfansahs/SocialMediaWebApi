using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;

namespace Media.Application.Features.Posts.Commands
{
    public class DeletePostCommand : IRequest<object>
    {
        public int PostId { get; set; }
        public string UserId { get; set; }

        public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, object>
        {

            private readonly IMapper _mapper;
            private readonly IPostRepository postRepository;

            public DeletePostCommandHandler(IMapper mapper, IPostRepository postRepository)
            {
                _mapper = mapper;
                this.postRepository = postRepository;
            }

            public async Task<object> Handle(DeletePostCommand request, CancellationToken cancellationToken)
            {

                var post = postRepository.AsQueryable()
                           .FirstOrDefault(i => i.UserId == request.UserId && i.Id == request.PostId);

                await postRepository.DeleteAsync(post);

                return null;
            }
        }
    }
}