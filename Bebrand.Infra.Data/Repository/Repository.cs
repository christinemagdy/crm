using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityInfo
    {
        protected readonly BebrandContext Db;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly IUser _user;
        public IUnitOfWork UnitOfWork => Db;
        public GenericRepository(BebrandContext context, IUser user)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
            _user = user;
        }
        public int SaveChanges()
        {
            return Db.SaveChanges();
        }
        public void UserStatus(Guid id, UserStatus status)
        {
            var Details = DbSet.Find(id);
            Details.Status = status;
            DbSet.Update(Details);
        }
        public async Task<QueryMultipleResult<TEntity>> Checke(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = DbSet;
            var result = new QueryMultipleResult<TEntity>();
            result.Data = await query.FirstOrDefaultAsync(filter);
            return result;
        }

        public virtual async Task<QueryMultipleResult<TEntity>> Add(TEntity obj)
        {
            try
            {
               
                obj.ModifiedBy = _user.GetUserId();
                await DbSet.AddAsync(obj);
                return new QueryMultipleResult<TEntity>(obj);
            }
            catch (Exception ex)
            {
                return new QueryMultipleResult<TEntity>(ex.Message);
            }
        }

        public virtual async Task<QueryMultipleResult<IEnumerable<TEntity>>> AddBulk(List<TEntity> obj)
        {
            try
            {
                obj.ForEach(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.ModifiedBy = _user.GetUserId();
                    x.ModifiedOn = DateTime.Now;
                    x.Status = Domain.Core.UserStatus.Active;
                });
                await DbSet.AddRangeAsync(obj);
                return new QueryMultipleResult<IEnumerable<TEntity>>(obj);
            }
            catch (Exception ex)
            {
                return new QueryMultipleResult<IEnumerable<TEntity>>(ex.Message);
            }
        }

        public virtual async Task<QueryMultipleResult<IEnumerable<TEntity>>> UpdateBulk(List<TEntity> obj)
        {
            try
            {
                obj.ForEach(x =>
                {
                    x.ModifiedBy = _user.GetUserId();
                    x.ModifiedOn = DateTime.Now;
                    x.Status = Domain.Core.UserStatus.Active;
                });
                await Task.Run(() =>
                {
                    DbSet.UpdateRange(obj);
                });

                return new QueryMultipleResult<IEnumerable<TEntity>>(obj);
            }
            catch (Exception ex)
            {
                return new QueryMultipleResult<IEnumerable<TEntity>>(ex.Message);
            }
        }
        public virtual async Task<QueryMultipleResult<TEntity>> GetById(Guid id, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var Result = new QueryMultipleResult<TEntity>();
            IQueryable<TEntity> query = DbSet;
            try
            {
                if (include != null)
                {
                    query = include(query);
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                var Data = await query.FirstOrDefaultAsync(x => x.Id == id);
                Result.Data = Data;
            }
            catch (Exception ex)
            {
                Result.Errors.Add(ex.Message);
            }
            return Result;
        }

        public virtual async Task<QueryMultipleResult<TEntity>> Update(TEntity obj)
        {
            try
            {
                obj.ModifiedBy = _user.GetUserId();
                var Data = await Task.Run(() =>
                {
                    DbSet.Update(obj);
                    return true;
                });
                return new QueryMultipleResult<TEntity>(obj);
            }
            catch (Exception ex)
            {
                return new QueryMultipleResult<TEntity>(ex.Message);
            }
        }

        public virtual async Task<QueryMultipleResult<bool>> Remove(Guid id)
        {
            try
            {
                var update = await DbSet.FindAsync(id);
                DbSet.Remove(update);
                return new QueryMultipleResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new QueryMultipleResult<bool>(ex.Message);
            }
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task<QueryMultipleResult<IEnumerable<TEntity>>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            {
                {
                    IQueryable<TEntity> query = DbSet;
                    var result = new QueryMultipleResult<IEnumerable<TEntity>>();

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    if (include != null)
                    {
                        query = include(query);
                    }

                    if (orderBy != null)
                    {
                        var data = await orderBy(query).ToListAsync();
                        result.Data = data;
                        result.Total = data.Count;
                        return result;
                    }
                    else
                    {
                        var data = await query.ToListAsync();
                        result.Data = data;
                        result.PageCount = data.Count;
                        return result;
                    }

                }
            }
        }

    }
}
