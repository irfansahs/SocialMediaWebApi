using AutoMapper;
using Media.Application.Dtos;
using Media.Application.Features.Posts.Dtos;
using Media.Application.Services;
using Media.Application.Services.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace Media.Application.Features.Comments.Commands
{
    public class CreateCommentCommand : IRequest<object>
    {
        public string UserId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }

        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, object>
        {

            private readonly ICommentRepository _repository;
            private readonly IMapper _mapper;
            private readonly IEmotionAnalyzeService _emotionAnalyzeService;
            private readonly ITagRepository _tagRepository;
            private readonly ITranslateService _translateService;


            public CreateCommentCommandHandler(ICommentRepository repository, IMapper mapper, ITagRepository tagRepository, IEmotionAnalyzeService emotionAnalyzeService, ITranslateService translateService)
            {
                _mapper = mapper;
                _repository = repository;
                _tagRepository = tagRepository;
                _emotionAnalyzeService = emotionAnalyzeService;
                _translateService = translateService;
            }


            public async Task<object> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {

                TranslateTextResponse translateTextResponse = await _translateService.TranslateText(request.Content);
                EmotionResponse emotionResponse = await _emotionAnalyzeService.GetEmotionAnalyzeAsync(translateTextResponse.Trans);

                var Comment = new Domain.Entities.Comment
                {
                    UserId = request.UserId,
                    Content = request.Content,
                    CreatedOn = DateTime.UtcNow,
                    PostId = request.PostId,
                    Emotion = emotionResponse.Emotion,
                    Polarity = emotionResponse.Polarity,
                    TranslatedPost = translateTextResponse.Trans,
                    SourceLanguageCode = translateTextResponse.source_language_code
                };

                await _repository.AddAsync(Comment);

                return null;
            }
        }
    }
}