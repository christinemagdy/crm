using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Repository
{
    public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
    {

        protected readonly IUser user;

        public TeamMemberRepository(BebrandContext db, IUser user) : base(db, user)
        {
            this.user = user;
        }


        public async Task<QueryResult<TeamMember>> GetAll()
        {
            var result = new QueryResult<TeamMember>();
            var Data = await DbSet.
              Include(x => x.TeamLeader).
               ThenInclude(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<QueryResult<TeamMember>> GetAllActive()
        {
            var result = new QueryResult<TeamMember>();
            var Data = await DbSet.
                Where(x => x.Status != Status.Deactivate).
                Include(x => x.TeamLeader).
                ThenInclude(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<QueryResult<TeamMember>> GetAllActivePerUser()
        {
            var result = new QueryResult<TeamMember>();
            var Data = await DbSet.
                Where(x => x.Status != Status.Deactivate && x.ModifiedBy == user.GetUserId() || x.TeamLeaderId == Guid.Parse(user.GetParentUserId())).
                Include(x => x.TeamLeader).
                ThenInclude(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<QueryResult<TeamMember>> GetAllDeleted()
        {
            var result = new QueryResult<TeamMember>();
            var Data = await DbSet.
                Where(x => x.Status == Status.Deactivate).
                Include(x => x.TeamLeader).
                 ThenInclude(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<QueryResult<TeamMember>> GetAvtiveUpdated()
        {
            var result = new QueryResult<TeamMember>();
            var Data = await DbSet.
                Where(x => x.Status == Status.Updated).
                Include(x => x.TeamLeader).
                 ThenInclude(x => x.SalesDirector).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public QueryMultipleResult<IQueryable<TeamMember>> GetByTeamLeaderId(Guid parentId)
        {
            return new QueryMultipleResult<IQueryable<TeamMember>>(DbSet.Where(x => x.TeamLeaderId == parentId && x.Status != Status.Deactivate));
        }
        public async Task<TeamMember> GetById(Guid id)
        {
            return await DbSet.Include(x => x.TeamLeader).ThenInclude(x => x.SalesDirector).FirstOrDefaultAsync(x => x.Id == id);
        }

        public QueryMultipleResult<IQueryable<TeamLeader>> GetTeamLeaderById(Guid parentId)
        {
            return new QueryMultipleResult<IQueryable<TeamLeader>>(DbSet.Include(x => x.TeamLeader).Where(x => x.Id == parentId).Select(s => s.TeamLeader));
        }
        public void Update(TeamMember TeamMember)
        {
            DbSet.Update(TeamMember);
        }

        public void UserStatus(Guid id, Status status)
        {
            var Details = GetById(id).Result;
            Details.Status = status;
            DbSet.Update(Details);
        }
    }
}
