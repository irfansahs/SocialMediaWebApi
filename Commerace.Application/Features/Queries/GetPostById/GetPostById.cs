using AutoMapper;
using Commerace.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllProducts
{
    public class GetPostById : IRequest<PostViewDto>
    {
        public int PostId { get; set; }

        public class GetPostByIdHandler : IRequestHandler<GetPostById, PostViewDto>
        {

            private readonly IPostRepository _repository;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;

            private readonly IMapper _mapper;


            public GetPostByIdHandler(IPostRepository repository, IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager)
            {
                _userManager = userManager;
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<PostViewDto> Handle(GetPostById request, CancellationToken cancellationToken)
            {

                var posts = await _repository.GetByIdAsync(request.PostId);

                var viewmodel = _mapper.Map<PostViewDto>(posts);

                return viewmodel;
            }
        }
    }
}
