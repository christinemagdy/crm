using Bebrand.Domain.Enums;
using Bebrand.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Interfaces
{
    public interface ITeamMemberRepository :  IRepository<TeamMember>, IGenericRepository<TeamMember>
    {
        Task<TeamMember> GetById(Guid id);
        Task<QueryResult<TeamMember>> GetAllActive();
        Task<QueryResult<TeamMember>> GetAllActivePerUser();
        QueryMultipleResult<IQueryable<TeamLeader>> GetTeamLeaderById(Guid parentId);
        Task<QueryResult<TeamMember>> GetAll();
        Task<QueryResult<TeamMember>> GetAvtiveUpdated();
        Task<QueryResult<TeamMember>> GetAllDeleted();
        QueryMultipleResult<IQueryable<TeamMember>> GetByTeamLeaderId(Guid parentId);
        void UserStatus(Guid id, Status status);
    }
}
