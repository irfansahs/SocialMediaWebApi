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
        public async Task<bool> GetFollowed(string FollowTo, string UserName)
        {
            var FollowExists = await _userDbContext.Follows
                .AnyAsync(x => x.FollowTo == FollowTo && x.UserName == UserName);

            return FollowExists;
        }
        public async Task<bool> DeleteFollow(string UserName,string FollowTo)
        {
            var followToDelete = await _userDbContext.Follows
       .FirstOrDefaultAsync(x => x.FollowTo == FollowTo && x.UserName == UserName);

            if (followToDelete != null)
            {
                _userDbContext.Follows.Remove(followToDelete);
                await _userDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> GetFollowsCount(string FollowTo)
        {
            var followCount = await _userDbContext.Follows.CountAsync(i => i.FollowTo == FollowTo);
            return followCount;
        }
    }
}