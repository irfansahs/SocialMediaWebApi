using Microsoft.AspNetCore.Identity;
using Media.Infrastructure.Contexts;
using Media.Domain.Entities;
using System.Threading.Tasks;
using Media.Application.Services.Repositories;
using Media.Infrastructure.Repositories;
using Media.Persistence.Repositories;

namespace Media.Infrastructure.Repositories
{
    public class FollowRepository : GenericRepository<Follow>, IFollowRepository
    {
        private UserDbContext _userDbContext;

        public FollowRepository(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }

    }
}