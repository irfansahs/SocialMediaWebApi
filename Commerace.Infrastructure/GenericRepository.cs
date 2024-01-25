using Bogus;
using Commerace.Application;
using Media.Application;
using Media.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Infrastructure
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
    }
}
