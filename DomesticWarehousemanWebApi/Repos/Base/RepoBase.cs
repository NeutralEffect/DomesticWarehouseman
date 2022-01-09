using DomesticWarehousemanWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace DomesticWarehousemanWebApi.Repos.Base
{
    public abstract class RepoBase<T> : IRepoBase<T> where T : class
    {
        protected DomesticWarehousemanDbContext _context { get; set; }

        public RepoBase(DomesticWarehousemanDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            if (_context == null)
            {
                return null;
            }

            return await _context.Set<T>()
                .FirstOrDefaultAsync(predicate);
        }

		public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
		{
            return _context.Set<T>().Where(predicate);
		}

        public async Task<T> Add(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);

            return result.Entity;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> DeleteByPredicate(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>()
                .FirstOrDefaultAsync(predicate);

            if (result == null)
            {
                return false;
            }

            _context.Set<T>().Remove(result);
            return true;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

		public IIncludableQueryable<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
		{
            return _context.Set<T>()
                .Include(navigationPropertyPath);
		}
	}
}
