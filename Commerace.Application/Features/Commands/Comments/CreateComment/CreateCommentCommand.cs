using AutoMapper;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Posts.CreatePost
{
    public class CreateCommentCommand : IRequest<object>
    {
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
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


                var Comment = new Comment
                {
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
