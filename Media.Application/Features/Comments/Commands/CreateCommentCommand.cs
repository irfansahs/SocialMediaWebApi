using AutoMapper;
using Media.Application.Features.Posts.Dtos;
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


            public CreateCommentCommandHandler(ICommentRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<object> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {

                using var client = new HttpClient();
                var url = "http://python_api:5010/analyze";
                var jsonContent = "{\"text\": \"" + request.Content + "\"}";
                var response = await client.PostAsync(url, new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"));
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Gelen cevap: " + responseString);

                var emotionResponse = JsonConvert.DeserializeObject<EmotionResponse>(responseString);

                var Comment = new Domain.Entities.Comment
                {
                    UserId = request.UserId,
                    Content = request.Content,
                    CreatedOn = DateTime.UtcNow,
                    PostId = request.PostId,
                    Emotion = emotionResponse.Emotion,
                    Polarity = emotionResponse.Polarity,
                };

                await _repository.AddAsync(Comment);

                return null;
            }
        }
    }
}