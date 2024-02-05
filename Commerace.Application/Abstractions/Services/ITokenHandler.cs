using Commerace.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
        string CreateRefreshToken();
    }
}
