using Commerace.Application;
using Commerace.Application.Dto;
using Media.Application.Abstractions.Services;
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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(UserDbContext userDbContext) : base(userDbContext)
        {
        }

    }
}
