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
    public class GetPostByUserNameQuery : IRequest<List<PostViewDto>>
    {
        public string UserName { get; set; }

        public class GetPostByUserNameHandler : IRequestHandler<GetPostByUserNameQuery, List<PostViewDto>>
        {

            private readonly IPostRepository _repository;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;

            private readonly IMapper _mapper;


            public GetPostByUserNameHandler(IPostRepository repository, IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager)
            {
                _userManager = userManager;
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<List<PostViewDto>> Handle(GetPostByUserNameQuery request, CancellationToken cancellationToken)
            {

                var posts = await _repository.GetByNameAsync(x => x.UserName == request.UserName);

                var viewmodel = _mapper.Map<List<PostViewDto>>(posts);

                return viewmodel;
            }
        }
    }
}
