using Media.Domain.Entities;


namespace Media.Application.Services.Repositories
{
    public interface IUserService
    {
        Task<bool> UpdateRefreshToken(string refreshToken, string Id,DateTime accessTokenDate,int refreshTokenLifeTime);
    }
}