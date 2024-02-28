using Microsoft.AspNetCore.Identity;
using Media.Infrastructure.Repositories;
using Media.Infrastructure.Contexts;
using Media.Domain.Entities;
using System.Threading.Tasks;
using Media.Application.Services.Repositories;
using Media.Persistence.Repositories;

namespace Media.Infrastructure.Repositories
{
     public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        private UserDbContext _userDbContext;

        public LikeRepository(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }



    }
}