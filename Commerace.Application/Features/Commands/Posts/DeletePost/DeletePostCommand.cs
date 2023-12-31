﻿using AutoMapper;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Posts.DeletePost
{
    public class DeletePostCommand : IRequest<object>
    {
        public int PostId { get; set; }


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
                var post = await postRepository.DeleteByIdAsync(request.PostId);

                return post;
            }
        }
    }
}
