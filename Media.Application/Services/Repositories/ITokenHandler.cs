
using Media.Application.Features.User.Dtos;

namespace Media.Application.Services.Repositories
{
   public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
        string CreateRefreshToken();
    }
}