using Bebrand.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Interfaces
{
    public interface IServiceRepository : IRepository<Service>, IGenericRepository<Service>, IDisposable
    {
        Task<QueryMultipleResult<IEnumerable<Service>>> IdList(List<Service> services);
    }
}
