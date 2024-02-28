using Media.Domain.Entities;
using Media.Application.Features;

namespace Media.Application.Services.Repositories
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<Tag> AddAsync(List<Tag> entity);

    }
}