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

      
    }
}
