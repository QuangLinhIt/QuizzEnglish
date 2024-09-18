using Microsoft.EntityFrameworkCore;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Common.Implement
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly DbSet<T> _dbSet;
        private readonly ApplicationDbContext _applicationDbContext;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _dbSet = applicationDbContext.Set<T>();
            _applicationDbContext = applicationDbContext;
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAsync(int pageIndex,int pageSize)
        {
            return await _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity); // theo dõi entity từ table T
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
