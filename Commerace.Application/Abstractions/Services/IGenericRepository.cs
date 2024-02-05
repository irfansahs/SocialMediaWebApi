using Media.Persistence.Dynamic;
using Media.Persistence.Page;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Abstractions.Services
{
    public interface IGenericRepository<T> where T : class
    {

        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetByNameAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteByIdAsync(int id);
        IQueryable<T> AsQueryable();

        IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                     Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                     int index = 0, int size = 10,
                     bool enableTracking = true);

        IPaginate<T> GetListByDynamic(Media.Persistence.Dynamic.Dynamic dynamic,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                      int index = 0, int size = 10, bool enableTracking = true);



    }
}
