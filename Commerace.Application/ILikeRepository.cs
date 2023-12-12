using Commerace.Application;
using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application
{
    public interface ILikeRepository : IGenericRepository<Like>
    {
        Task<int> GetLikesCount(int PostId);
        Task<bool> GetLiked(int PostId, string UserName);
        Task<bool> DeleteLike(int PostId, string UserName);


    }
}
