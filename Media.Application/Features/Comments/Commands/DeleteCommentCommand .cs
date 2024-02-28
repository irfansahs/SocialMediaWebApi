using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;

namespace Media.Application.Features.Comments.Commands
{
    public class DeleteCommentCommand : IRequest<object>
    {
        public int CommentId { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, object>
        {

            private readonly IMapper _mapper;
            private readonly ICommentRepository commentRepository;

            public DeleteCommentCommandHandler(IMapper mapper, ICommentRepository commentRepository)
            {
                _mapper = mapper;
                this.commentRepository = commentRepository;
            }

            public async Task<object> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await commentRepository.DeleteByIdAsync(request.CommentId);

                return comment;
            }
        }
    }
}