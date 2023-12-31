﻿using AutoMapper;
using Commerace.Application.Features.Queries.GetAllProducts;
using Media.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Commands.Like.CreateLike
{
    public class CreateLikeCommand : IRequest<object>
    {
        public string UserName { get; set; }
        public int PostId { get; set; }

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

                var Like = new Media.Domain.Like
                {
                    UserName = request.UserName,
                    PostId = request.PostId,
                };

                await _repository.AddAsync(Like);

                return null;
            }


        }

    }
}
