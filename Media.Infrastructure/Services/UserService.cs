using Media.Application.Services.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Media.Infrastructure.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Media.Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<Media.Domain.Entities.Identity.AppUser> appUser)
        {
            _userManager = appUser;
        }

        public async Task<bool> UpdateRefreshToken(string refreshToken, string Id, DateTime accessTokenDate, int refreshTokenLifeTime)
        {
            Media.Domain.Entities.Identity.AppUser user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                user.RefhreshToken = refreshToken;
                user.RefhreshTokenEndDate = accessTokenDate.AddMinutes(refreshTokenLifeTime);
                await _userManager.UpdateAsync(user);
            }


            return true;
        }
    }
}