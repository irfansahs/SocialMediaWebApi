using Media.Domain.Entities;


namespace Media.Application.Services
{
    public interface IUserService
    {
        Task<bool> UpdateRefreshToken(string refreshToken, string Id,DateTime accessTokenDate,int refreshTokenLifeTime);
    }
}