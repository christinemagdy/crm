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
    public interface ITeamLeaderRepository : IRepository<TeamLeader>, IGenericRepository<TeamLeader>
    {
        Task<TeamLeader> GetById(Guid id);

        Task<QueryResult<TeamLeader>> GetAllActive();
        Task<QueryResult<TeamLeader>> GetAll();
        Task<QueryResult<TeamLeader>> GetAvtiveUpdated();

        Task<QueryResult<TeamLeader>> GetAllDeleted();
        Task<QueryResult<TeamLeader>> GetAllActivePerUser();
        QueryMultipleResult<IQueryable<Customer>> GetSlaesDirectorById(Guid parentId);
        QueryMultipleResult<IQueryable<TeamLeader>> GetBySlaesDirectorId(Guid parentId);
        void UserStatus(Guid id, Status status);

    }



}
