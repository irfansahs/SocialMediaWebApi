using Microsoft.AspNetCore.Identity;
using Media.Infrastructure.Repositories;
using Media.Infrastructure.Contexts;
using Media.Domain.Entities;
using System.Threading.Tasks;
using Media.Application.Services.Repositories;

namespace Media.Persistence.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {

        private UserDbContext _userDbContext;

        public TagRepository(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }

         Task<Tag> ITagRepository.AddAsync(List<Tag> entity)
        {
            _userDbContext.Tags.AddRangeAsync(entity);
            _userDbContext.SaveChangesAsync();

            return null;
        }


    }
}