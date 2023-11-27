using Commerace.Application;
using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Infrastructure
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(UserDbContext userDbContext) : base(userDbContext)
        {
        }
    }
}
