using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        int GetLike(int postId);
        List<Post> GetByContent(string content);
        Task<List<Post>> GetPost(string name);

    }
}
