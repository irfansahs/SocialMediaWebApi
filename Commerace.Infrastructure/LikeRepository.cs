using Commerace.Application;
using Commerace.Infrastructure;
using Media.Application;
using Media.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Infrastructure
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
