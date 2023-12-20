using Commerace.Application;
using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application
{
    public interface IFollowRepository : IGenericRepository<Follow>
    {
        Task<int> GetFollowsCount(string FollowTo);
        Task<bool> GetFollowed(string FollowTo, string UserName);
        Task<bool> DeleteFollow(string FollowTo, string UserName);
    }
}
