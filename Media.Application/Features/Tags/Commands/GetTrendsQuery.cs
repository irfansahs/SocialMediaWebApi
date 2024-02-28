using AutoMapper;
using Media.Application.Features.Tags.Dtos;
using Media.Application.Services.Repositories;
using Media.Persistence.Dynamic;
using MediatR;

namespace Media.Application.Features.Tags.Commands
{
    public class GetTrendsQuery : IRequest<List<TrendsResponseDto>>
    {
        public Dynamic dynamic { get; set; }

        public class GetTrendsQueryHandler : IRequestHandler<GetTrendsQuery, List<TrendsResponseDto>>
        {

            private readonly ITagRepository _tagRepository;
            private readonly IMapper _mapper;

            public GetTrendsQueryHandler(ITagRepository tagRepository, IMapper mapper)
            {
                _mapper = mapper;
                _tagRepository = tagRepository;
            }


            public async Task<List<TrendsResponseDto>> Handle(GetTrendsQuery request, CancellationToken cancellationToken)
            {
                var trends = await _tagRepository.GetAllAsync();

                var trendingTags = trends
                    .GroupBy(tag => tag.Name)
                    .Select(group => new TrendsResponseDto
                    {
                        Name = group.Key,
                        Count = group.Count()
                    })
                    .OrderByDescending(tagGroup => tagGroup.Count)
                    .Take(5)
                    .ToList();

                return null;
            }
        }
    }
}