using Media.Application.Abstractions.Services;
using Media.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Infrastructure.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Media.Domain.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> appUser)
        {
            _userManager = appUser;
        }

        public async Task<bool> UpdateRefreshToken(string refreshToken, string Id, DateTime accessTokenDate, int refreshTokenLifeTime)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);

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
