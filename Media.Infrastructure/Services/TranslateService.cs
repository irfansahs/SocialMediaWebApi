using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Media.Application.Dtos;
using Media.Application.Services;
using Newtonsoft.Json;

namespace Media.Infrastructure.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly HttpClient _client;


        public TranslateService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "a36c358733msh95346e9f8153641p10252djsn5a91e2ad5857");
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "google-translate113.p.rapidapi.com");
        }

        public async Task<TranslateTextResponse> TranslateText(string content)
        {
            var request = new Dictionary<string, string>
            {
                { "from", "auto" },
                { "to", "en" },
                { "text", content },
            };

            var response = await _client.PostAsync("https://google-translate113.p.rapidapi.com/api/v1/translator/text", new FormUrlEncodedContent(request));
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);

          

            var translateResponse = JsonConvert.DeserializeObject<TranslateTextResponse>(responseBody);

            return translateResponse;
        }
    }
}