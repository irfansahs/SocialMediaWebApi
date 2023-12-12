using Commerace.Application;
using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Infrastructure
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(UserDbContext userDbContext) : base(userDbContext)
        {
        }
        private readonly UserDbContext _userDbContext;

        public int GetLike(int postId)
        {

            var likeCounts = _userDbContext.Posts
            .Where(p => p.Id == postId)
            .Select(p => p.Likes.Count)
            .FirstOrDefault();

            return likeCounts;
        }

    }
}
