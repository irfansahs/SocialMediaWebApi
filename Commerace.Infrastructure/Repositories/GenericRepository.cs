using Bogus;
using Commerace.Application;
using Media.Application;
using Media.Application.Abstractions.Services;
using Media.Domain;
using Media.Infrastructure.Contexts;
using Media.Persistence.Dynamic;
using Media.Persistence.Page;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Media.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UserDbContext userDbContext;

        public GenericRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await userDbContext.Set<T>().AddAsync(entity);
            await userDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await userDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await userDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> DeleteByIdAsync(int id)
        {
            var user = await userDbContext.Set<T>().FindAsync(id);
            userDbContext.Set<T>().Remove(user);
            userDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<List<T>> GetByNameAsync(Expression<Func<T, bool>> filter)
        {
            return await userDbContext.Set<T>().Where(filter).ToListAsync();
        }

        public IQueryable<T> AsQueryable()
        {
            return userDbContext.Set<T>().AsQueryable();
        }
        public async Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await userDbContext.Set<T>().Where(filter).AsQueryable().ToListAsync();
        }

        public async Task<T> DeleteAsync(T entity)
        {
            userDbContext.Set<T>().Remove(entity);
            await userDbContext.SaveChangesAsync();
            return entity;
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                      int index = 0, int size = 10,
                                      bool enableTracking = true)
        {
            IQueryable<T> queryable = Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return orderBy(queryable).ToPaginate(index, size);
            return queryable.ToPaginate(index, size);
        }

        public IQueryable<T> Query()
        {
            return userDbContext.Set<T>();
        }

        public IPaginate<T> GetListByDynamic(Dynamic dynamic,
                                                   Func<IQueryable<T>, IIncludableQueryable<T, object>>?
                                                       include = null, int index = 0, int size = 10,
                                                   bool enableTracking = true)
        {
            IQueryable<T> queryable = Query().AsQueryable().ToDynamic(dynamic);
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return queryable.ToPaginate(index, size);
        }
    }
}
