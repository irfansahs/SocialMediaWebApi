using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;

namespace Media.Application.Features.Comments.Commands
{
    public class CreateCommentCommand : IRequest<object>
    {
        public string UserId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }

        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, object>
        {

            private readonly ICommentRepository _repository;
            private readonly IMapper _mapper;


            public CreateCommentCommandHandler(ICommentRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {


                var Comment = new Domain.Entities.Comment
                {
                    UserId = request.UserId,
                    Content = request.Content,
                    CreatedOn = DateTime.Now,
                    PostId = request.PostId,
                };

                await _repository.AddAsync(Comment);

                return null;
            }
        }
    }
}