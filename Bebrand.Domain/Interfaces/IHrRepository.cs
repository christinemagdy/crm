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
    public interface IHrRepository : IRepository<Hr>, IGenericRepository<Hr>
    {
        Task<Hr> GetById(Guid id);
        Task<Hr> GetByEmail(string email);
        Task<QueryResult<Hr>> GetAll();
        Task<QueryResult<Hr>> GetAllActive();
        Task<QueryResult<Hr>> GetAllActiveByUser();
        void UserStatus(Guid id, Status status);
        Task<QueryResult<Hr>> GetAvtiveUpdated();
        Task<QueryResult<Hr>> GetAllDeleted();
        void Remove(Hr customer);
    }
}
