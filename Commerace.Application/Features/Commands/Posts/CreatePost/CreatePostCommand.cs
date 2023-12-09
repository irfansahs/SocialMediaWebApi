using AutoMapper;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Posts.CreatePost
{
    public class CreatePostCommand : IRequest<object>
    {
        public string UserName { get; set; }
        public string Content { get; set; }

        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, object>
        {

            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;
            private readonly UserManager<Media.Domain.Identity.AppUser> _userManager;

            public CreatePostCommandHandler(IPostRepository repository, IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager)
            {
                _mapper = mapper;
                _repository = repository;
                _userManager = userManager;
            }


            public async Task<object> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(a => a.UserName == request.UserName);


                var post = new Post
                {
                    UserName = user.UserName,
                    CreatedOn = DateTime.Now,
                    Content = request.Content
                };

                await _repository.AddAsync(post);

                return null;
            }
        }
    }
}
