using AutoMapper;
using Commerace.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllProducts
{
    public class GetAllPostsQuery : IRequest<List<PostViewDto>>
    {
        public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, List<PostViewDto>>
        {

            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;


            public GetAllPostsQueryHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<List<PostViewDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
            {
                var posts = await _repository.GetAllAsync();

                var viewmodel = _mapper.Map<List<PostViewDto>>(posts);
                
                return viewmodel;
            }
        }
    }
}
