using Commerace.Application;
using Commerace.Infrastructure;
using Media.Application;
using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Infrastructure
{
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        public LikeRepository(UserDbContext userDbContext) : base(userDbContext)
        {
        }

    }
}
