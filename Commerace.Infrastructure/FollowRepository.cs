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
    public class FollowRepository : GenericRepository<Follow>, IFollowRepository
    {
        private UserDbContext _userDbContext;

        public FollowRepository(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }
    
    }
}