using Microsoft.AspNetCore.Identity;
using Media.Infrastructure.Repositories;
using Media.Infrastructure.Contexts;
using Media.Domain.Entities;
using System.Threading.Tasks;
using Media.Application.Services.Repositories;

namespace Media.Persistence.Repositories
{
     public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(UserDbContext userDbContext) : base(userDbContext)
        {
        }

    }
}