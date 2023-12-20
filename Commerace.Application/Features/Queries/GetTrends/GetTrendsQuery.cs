using AutoMapper;
using Commerace.Application;
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

            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;


            public GetTrendsQueryHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<List<TrendsResponseDto>> Handle(GetTrendsQuery request, CancellationToken cancellationToken)
            {

                var posts = await _repository.GetAllAsync();




                var wordLists = posts.Select(post => post.Content.Split(new char[] { ' ', ',', '.', ';' }, StringSplitOptions.RemoveEmptyEntries));

                // Her kelimenin sayısını hesaplamak için bir Lambda ifadesi kullanın
                var wordCounts = wordLists.Select(words => words.Where(word => word.StartsWith("#")).Count());
                var hashtagCount = posts.Where(post => post.Content.StartsWith("#")).Count();

                var viewmodel = _mapper.Map<List<TrendsResponseDto>>(posts);


                /*
                foreach (var post in viewmodel)
                {
                    post.Tag = posts.Select(post => post.Content.Split(new char[] { ' ', ',', '.', ';' }, StringSplitOptions.RemoveEmptyEntries));
                    post.Counts = Convert.ToInt32(wordLists.Select(words => words.Where(word => word.StartsWith("#")).Count()));

                }
                */

                Console.WriteLine(hashtagCount);

                return null;
            }
        }
    }


}
