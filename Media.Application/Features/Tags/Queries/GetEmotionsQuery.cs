using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;

namespace Media.Application.Features.Tags.Queries
{
    public class GetEmotionsQuery : IRequest<object>
    {

        public class GetEmotionsQueryHandler : IRequestHandler<GetEmotionsQuery, object>
        {

            private readonly IPostRepository _postRepository;
            private readonly IMapper _mapper;

            public GetEmotionsQueryHandler(IPostRepository postRepository, IMapper mapper)
            {
                _mapper = mapper;
                _postRepository = postRepository;
            }

            public async Task<object> Handle(GetEmotionsQuery request, CancellationToken cancellationToken)
            {


                var trends = _postRepository.AsQueryable().GroupBy(a => a.Emotion).Select(group => new
                {
                    emotion = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(tag => tag.Count)
                .ToList();
        
                return trends;
            }
        }

    }
}