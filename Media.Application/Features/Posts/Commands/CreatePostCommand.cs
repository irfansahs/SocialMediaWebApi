using AutoMapper;
using Media.Application.Dtos;
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
            private readonly ITranslateService _translateService;
            private readonly IMapper _mapper;


            public CreatePostCommandHandler(IPostRepository repository, IMapper mapper, ITagRepository tagRepository, IEmotionAnalyzeService emotionAnalyzeService, ITranslateService translateService)
            {
                _mapper = mapper;
                _repository = repository;
                _tagRepository = tagRepository;
                _emotionAnalyzeService = emotionAnalyzeService;
                _translateService = translateService;
            }

            public async Task<object> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                TranslateTextResponse translateTextResponse = await _translateService.TranslateText(request.Content);
                EmotionResponse emotionResponse = await _emotionAnalyzeService.GetEmotionAnalyzeAsync(translateTextResponse.Trans);

                var post = new Post
                {
                    UserId = request.UserId,
                    CreatedOn = DateTime.UtcNow,
                    Content = request.Content,
                    Emotion = emotionResponse.Emotion,
                    Polarity = emotionResponse.Polarity,
                    TranslatedPost = translateTextResponse.Trans,
                    SourceLanguageCode = translateTextResponse.source_language_code
                };


                Regex regex = new Regex(@"#[\w]*");

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