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
    public class CreatePostCommand : IRequest<object>
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Comments { get; set; }


        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, object>
        {

            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;


            public CreatePostCommandHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                Media.Domain.Post post = _mapper.Map<Media.Domain.Post>(request);
                await _repository.AddAsync(post);


                return null;
            }
        }
    }
}
