﻿using AutoMapper;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Queries.GetTrends
{


    public class GetTrendsQuery : IRequest<List<TrendsResponseDto>>
    {
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

                return trendingTags;
            }
        }
    }


}
