using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Media.Application.Dtos
{
    public class TranslateTextResponse
    {
        public string Trans { get; set; }
        public string source_language_code { get; set; }
        public string source_language { get; set; }
        public double trust_level { get; set; }
    }
}