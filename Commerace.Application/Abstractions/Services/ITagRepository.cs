using Commerace.Application;
using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Abstractions.Services
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<Tag> AddAsync(List<Tag> entity);

    }
}
