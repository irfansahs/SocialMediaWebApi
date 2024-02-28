using Microsoft.AspNetCore.Identity;
using Media.Persistence.Repositories;
using Media.Infrastructure.Contexts;
using Media.Domain.Entities;
using System.Threading.Tasks;
using Media.Application.Services.Repositories;

namespace Media.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private UserDbContext _userDbContext;

        public CommentRepository(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }


    }
}