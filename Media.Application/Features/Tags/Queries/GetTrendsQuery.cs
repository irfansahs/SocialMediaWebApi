using AutoMapper;
using Media.Application.Features.Tags.Dtos;
using Media.Application.Services.Repositories;
using MediatR;

namespace Media.Application.Features.Tags.Queries
{
    public class GetTrendsQuery : IRequest<object>
    {

        public class GetTrendsQueryHandler : IRequestHandler<GetTrendsQuery, object>
        {

            private readonly ITagRepository _tagRepository;
            private readonly IMapper _mapper;

            public GetTrendsQueryHandler(ITagRepository tagRepository, IMapper mapper)
            {
                _mapper = mapper;
                _tagRepository = tagRepository;
            }

            public async Task<object> Handle(GetTrendsQuery request, CancellationToken cancellationToken)
            {


                var trends = _tagRepository.AsQueryable()
            .GroupBy(tag => tag.Name)
            .Select(group => new
            {
                Name = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(tag => tag.Count)
            .Take(5)
            .ToList();

                // Sonucu döndürüyoruz
                return trends;
            }
        }
    }
}