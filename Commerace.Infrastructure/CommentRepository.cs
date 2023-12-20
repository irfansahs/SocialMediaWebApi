using Commerace.Application;
using Commerace.Application.Dto;
using Media.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Infrastructure
{
    public class CommentRepository : GenericRepository<Comment> , ICommentRepository
    {
        private UserDbContext _userDbContext;

        public CommentRepository(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<int> GetCommentsCount(int PostId)
        {
            var CommentsCount = await _userDbContext.Comments.CountAsync(i => i.PostId == PostId);
            return CommentsCount;
        }
    }
}
