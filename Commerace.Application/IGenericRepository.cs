using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application
{
    public interface IGenericRepository<T> where T : class
    {
        
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetByNameAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<T> DeleteByIdAsync(int id);
    }
}
