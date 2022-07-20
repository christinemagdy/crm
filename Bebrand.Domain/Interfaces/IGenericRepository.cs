using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void UserStatus(Guid id, UserStatus status);

        int SaveChanges();

        Task<QueryMultipleResult<TEntity>> Checke(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<QueryMultipleResult<TEntity>> Add(TEntity obj);
        Task<QueryMultipleResult<IEnumerable<TEntity>>> AddBulk(List<TEntity> obj);
        Task<QueryMultipleResult<IEnumerable<TEntity>>> UpdateBulk(List<TEntity> obj);
        Task<QueryMultipleResult<TEntity>> GetById(Guid id, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<QueryMultipleResult<TEntity>> Update(TEntity obj);
        Task<QueryMultipleResult<bool>> Remove(Guid id);

        Task<QueryMultipleResult<IEnumerable<TEntity>>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    }
}
