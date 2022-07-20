using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bebrand.Domain.Enums;

namespace Bebrand.Infra.Data.Repository
{
    public class TeamLeaderRepository : GenericRepository<TeamLeader>, ITeamLeaderRepository
    {
        protected readonly IUser user;
        public TeamLeaderRepository(BebrandContext context, IUser user) : base(context, user)
        {
            this.user = user;
        }


        public async Task<QueryResult<TeamLeader>> GetAllActive()
        {
            var result = new QueryResult<TeamLeader>();
            var Data = await DbSet.
                Where(x => x.Status != Status.Deactivate).
                Include(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }

        public async Task<QueryResult<TeamLeader>> GetAllActivePerUser()
        {
            var result = new QueryResult<TeamLeader>();
            var Data = await DbSet.
                Where(x => x.Status != Status.Deactivate && x.ModifiedBy == user.GetUserId() || x.SalesDirectorId == Guid.Parse(user.GetParentUserId())).
                Include(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<QueryResult<TeamLeader>> GetAvtiveUpdated()
        {
            var result = new QueryResult<TeamLeader>();
            var Data = await DbSet.
                Where(x => x.Status == Status.Updated).
                Include(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }

        public async Task<QueryResult<TeamLeader>> GetAllDeleted()
        {
            var result = new QueryResult<TeamLeader>();
            var Data = await DbSet.
                Where(x => x.Status == Status.Deactivate).
                Include(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<QueryResult<TeamLeader>> GetAll()
        {
            var result = new QueryResult<TeamLeader>();
            var Data = await DbSet.
                Include(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }

        public async Task<TeamLeader> GetById(Guid id)
        {
            return await DbSet.Include(x => x.SalesDirector).FirstOrDefaultAsync(x => x.Id == id);
        }
        public void UserStatus(Guid id, Status status)
        {
            var Details = GetById(id).Result;
            Details.Status = status;
            DbSet.Update(Details);

        }

        public QueryMultipleResult<IQueryable<TeamLeader>> GetBySlaesDirectorId(Guid parentId)
        {
            var data = DbSet.Where(x => x.SalesDirectorId == parentId && x.Status != Status.Deactivate);
            return new QueryMultipleResult<IQueryable<TeamLeader>>(data);
        }
        public QueryMultipleResult<IQueryable<Customer>> GetSlaesDirectorById(Guid parentId)
        {
            var data = DbSet.Include(x => x.SalesDirector).Where(x => x.Id == parentId).Select(x => x.SalesDirector);
            return new QueryMultipleResult<IQueryable<Customer>>(data);
        }

    }
}
