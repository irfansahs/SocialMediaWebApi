using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Media.Application.Features.Posts.Dtos;

namespace Media.Application.Services
{
    public interface IEmotionAnalyzeService
    {
        Task<EmotionResponse> GetEmotionAnalyzeAsync(string Content);
    }
}