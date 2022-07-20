using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Repository
{
    public class ServiceProviderRepository : GenericRepository<ServiceProvider>, IServiceProviderRepository
    {
        public ServiceProviderRepository(BebrandContext context, IUser user) : base(context, user)
        {

        }

        public async Task<QueryMultipleResult<IEnumerable<ServiceProvider>>> UpdateBasedOnClient(List<ServiceProvider> serviceProviders, Guid ClientId)
        {
            try
            {
                serviceProviders.ForEach(x =>
                {
                    x.ModifiedOn = DateTime.Now;
                    x.ModifiedBy = _user.GetUserId();

                });


                await Task.Run(() => { DbSet.RemoveRange(DbSet.Where(x => x.ClientID == ClientId)); });
                await DbSet.AddRangeAsync(serviceProviders);
                return new QueryMultipleResult<IEnumerable<ServiceProvider>>(serviceProviders);
            }
            catch (Exception ex)
            {
                return new QueryMultipleResult<IEnumerable<ServiceProvider>>(ex.Message);
            }

        }
    }

}
