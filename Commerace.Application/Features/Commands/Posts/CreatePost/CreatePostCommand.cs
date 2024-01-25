using AutoMapper;
using Bogus.DataSets;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Domain;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Posts.CreatePost
{
    public class CreatePostCommand : IRequest<object>
    {
        public string UserId { get; set; }
        public string Content { get; set; }

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
                var post = new Post
                {
                    UserId = request.UserId,
                    CreatedOn = DateTime.Now,
                    Content = request.Content
                };

                await _repository.AddAsync(post);

                return null;
            }
        }
    }
}
