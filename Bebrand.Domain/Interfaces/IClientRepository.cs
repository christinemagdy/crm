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
    public interface IClientRepository : IRepository<Client>, IGenericRepository<Client>
    {
        Task<Client> GetById(Guid id);
        Task<Client> GetByEmail(string email);
        Task<bool> IfkeyExistence(string key, bool user);
        QueryResult<Client> GetAll(bool User, OwnerParameters ownerParameters, string key = null);
        QueryResult<Client> GetAllActive(bool User, OwnerParameters ownerParameters, string key = null);

        QueryResult<Client> GetAvtiveUpdated(bool User, OwnerParameters ownerParameters, string key = null);

        QueryResult<Client> GetAllDeleted(bool User, OwnerParameters ownerParameters, string key = null);

        void AddBulk(List<Client> clients);

        Task<QueryMultipleResult<IEnumerable<Client>>> ClientsPerTeam(OwnerParameters ownerParameters, string key = null);


    }
}
