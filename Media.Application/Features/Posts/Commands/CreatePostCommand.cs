using AutoMapper;
using Media.Application.Features.Posts.Dtos;
using Media.Application.Services;
using Media.Application.Services.Repositories;
using Media.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Media.Application.Features.Posts.Commands
{
    public class CreatePostCommand : IRequest<object>
    {
        public string UserId { get; set; }
        public string Content { get; set; }

        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, object>
        {

            private readonly IPostRepository _repository;
            private readonly IEmotionAnalyzeService _emotionAnalyzeService;
            private readonly ITagRepository _tagRepository;
            private readonly IMapper _mapper;


            public CreatePostCommandHandler(IPostRepository repository, IMapper mapper, ITagRepository tagRepository,IEmotionAnalyzeService emotionAnalyzeService)
            {
                _mapper = mapper;
                _repository = repository;
                _tagRepository = tagRepository;
                _emotionAnalyzeService = emotionAnalyzeService;
            }

            public async Task<object> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {

                EmotionResponse emotionResponse = await _emotionAnalyzeService.GetEmotionAnalyzeAsync(request.Content);

                var post = new Post
                {
                    UserId = request.UserId,
                    CreatedOn = DateTime.UtcNow,
                    Content = request.Content,
                    Emotion = emotionResponse.Emotion,
                    Polarity = emotionResponse.Polarity,
                };


                // Hashtag içeren kelimeleri bulmak için düzenli ifade
                Regex regex = new Regex(@"#[\w]*");

                // Tüm eşleşmeleri bulmak için Matches metodu kullanılır
                MatchCollection matches = regex.Matches(request.Content);

                Tag tag = new Tag();

                foreach (Match match in matches)
                {

                    tag.Name = match.Value;
                    await _tagRepository.AddAsync(tag);
                }

                await _repository.AddAsync(post);

                return null;
            }
        }
    }
}