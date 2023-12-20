using Commerace.Application;
using Commerace.Application.Dto;
using Media.Domain;
using Microsoft.EntityFrameworkCore;
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

        public List<Post> GetByContent(string content)
        {
            return _userDbContext.Posts
                .Where(x => x.Content.Contains(content, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public async Task<List<Post>> GetPost(string name)
        {
            List<Post> posts = await _userDbContext.Posts.ToListAsync();
            
            return posts;
        }
    }
}
