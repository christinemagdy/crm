using Bebrand.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Interfaces
{
    public interface IServiceProviderRepository : IRepository<ServiceProvider>, IGenericRepository<ServiceProvider>
    {
        Task<QueryMultipleResult<IEnumerable<ServiceProvider>>> UpdateBasedOnClient(List<ServiceProvider> serviceProviders, Guid ClientId);
    }
}
