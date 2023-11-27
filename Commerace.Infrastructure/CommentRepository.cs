using Commerace.Application;
using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Infrastructure
{
    public class CommentRepository : GenericRepository<Comment> , ICommentRepository
    {
        public CommentRepository(UserDbContext userDbContext) : base(userDbContext)
        {
        }
    }
}
