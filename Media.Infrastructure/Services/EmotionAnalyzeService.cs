using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Media.Application.Features.Posts.Dtos;
using Media.Application.Services;
using Newtonsoft.Json;

namespace Media.Infrastructure.Services
{
    public class EmotionAnalyzeService : IEmotionAnalyzeService
    {
        public async Task<EmotionResponse> GetEmotionAnalyzeAsync(string Content)
        {
            using var client = new HttpClient();
            // var url = configuration.GetConnectionString("emotionApiUrl");
            var url = "http://python_api:5010/analyze";
            var jsonContent = "{\"text\": \"" + Content + "\"}";
            var response = await client.PostAsync(url, new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Gelen cevap: " + responseString);

            var emotionResponse = JsonConvert.DeserializeObject<EmotionResponse>(responseString);

            return emotionResponse;
        }


    }
}