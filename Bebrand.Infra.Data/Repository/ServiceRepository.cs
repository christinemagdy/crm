using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Repository
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {

        public ServiceRepository(BebrandContext context, IUser user) : base(context, user)
        {

        }

        public async Task<QueryMultipleResult<IEnumerable<Service>>> IdList(List<Service> services)
        {
            var data = await DbSet.ToListAsync();
            var result = new QueryMultipleResult<IEnumerable<Service>>();
            services.ForEach(x =>
            {
                data.ForEach(y =>
                {
                    if (y.Name == x.Name)
                    {
                        x.Id = y.Id;
                    }
                });
            });


            services.ForEach(x =>
            {
                var Existence = data.Select(a => a.Name).Contains(x.Name);
                if (!Existence)
                {
                    result.Errors.Add(x.Name + " " + "Not Existence");
                }
            });
            result.Data = services;
            return result;
        }
    }
}
