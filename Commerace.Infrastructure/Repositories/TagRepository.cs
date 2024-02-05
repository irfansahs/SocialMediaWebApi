using Bogus.DataSets;
using Media.Application;
using Media.Application.Abstractions.Services;
using Media.Application.Dto;
using Media.Domain;
using Media.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Infrastructure.Repositories
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
