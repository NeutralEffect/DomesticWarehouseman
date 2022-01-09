using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace DomesticWarehousemanWebApi.Repos.Base
{
    public interface IRepoBase<T> where T : class
    {
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        Task<T> Add(T entity);

        void Update(T entity);

        Task<bool> DeleteByPredicate(Expression<Func<T, bool>> predicate);

        Task<int> SaveChanges();

        IIncludableQueryable<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath);
    }
}
