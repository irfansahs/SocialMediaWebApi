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

        public async Task<bool> GetLiked(int PostId, string UserName)
        {
            var likeExists = await _userDbContext.Likes
                .AnyAsync(like => like.PostId == PostId && like.UserName == UserName);

            return likeExists;
        }
        public async Task<bool> DeleteLike(int PostId, string UserName)
        {
            var likeToDelete = await _userDbContext.Likes
       .FirstOrDefaultAsync(like => like.PostId == PostId && like.UserName == UserName);

            if (likeToDelete != null)
            {
                _userDbContext.Likes.Remove(likeToDelete);
                await _userDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> GetLikesCount(int postId)
        {
            var likesCount = await _userDbContext.Likes.CountAsync(i => i.PostId == postId);
            return likesCount;
        }

    }
}
