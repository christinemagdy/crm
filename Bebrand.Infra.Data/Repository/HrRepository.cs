using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Repository
{
   public class HrRepository : GenericRepository<Hr>, IHrRepository
    {

        protected readonly IUser user;

        public HrRepository(BebrandContext context, IUser user) : base(context, user)
        {
            this.user = user;
        }


        public async Task<Hr> GetById(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<QueryResult<Hr>> GetAllActive()
        {
            var result = new QueryResult<Hr>();
            var Data = await DbSet.
                Where(x => x.Status != Status.Deactivate).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }
        public async Task<QueryResult<Hr>> GetAllActiveByUser()
        {
            var result = new QueryResult<Hr>();
            var Data = await DbSet.
                Where(x => x.Status != Status.Deactivate && x.ModifiedBy == user.GetUserId()).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }
        public async Task<QueryResult<Hr>> GetAvtiveUpdated()
        {
            var result = new QueryResult<Hr>();
            var Data = await DbSet.
                Where(x => x.Status == Status.Updated).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }

        public async Task<QueryResult<Hr>> GetAllDeleted()
        {
            var result = new QueryResult<Hr>();
            var Data = await DbSet.
                Where(x => x.Status == Status.Deactivate).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }

        public async Task<QueryResult<Hr>> GetAll()
        {
            var result = new QueryResult<Hr>();
            var Data = await DbSet.ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<Hr> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public void UserStatus(Guid id, Status status)
        {
            var Details = GetById(id).Result;
            Details.Status = status;
            DbSet.Update(Details);

        }

        public void Remove(Hr hr)
        {
            DbSet.Remove(hr);
        }


    }
}