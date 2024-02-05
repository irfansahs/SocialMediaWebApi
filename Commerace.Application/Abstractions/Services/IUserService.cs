﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> UpdateRefreshToken(string refreshToken, string Id,DateTime accessTokenDate,int refreshTokenLifeTime);
    }
}
