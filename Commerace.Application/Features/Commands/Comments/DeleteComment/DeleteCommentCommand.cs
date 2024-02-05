using AutoMapper;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Application.Abstractions.Services;
using Media.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Posts.DeletePost
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
