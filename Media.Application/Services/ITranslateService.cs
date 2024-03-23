using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Media.Application.Dtos;

namespace Media.Application.Services
{
    public interface ITranslateService
    {
        Task<TranslateTextResponse> TranslateText(string Content);
    }
}