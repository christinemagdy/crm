using Bebrand.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Interfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        Task<Area> GetById(Guid id);

        Task<Area> GetByName(string Name);
        Task<QueryResult<Area>> GetAll();
        void Add(Area Area);
        void Update(Area Area);
        void Remove(Area Area);
        Task<bool> IfAreaExist(string Name);
    }
}

