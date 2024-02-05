using Commerace.Application;
using Media.Application;
using Media.Application.Abstractions.Services;
using Media.Domain;
using Media.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Infrastructure.Repositories
{
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        private UserDbContext _userDbContext;

        public LikeRepository(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }



    }
}
