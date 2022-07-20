using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Repository
{
    public class JobsRepository : GenericRepository<Jobs>, IJobsRepository
    {
        private readonly BebrandContext _context;
        public JobsRepository(BebrandContext context, IUser user) : base(context, user)
        {
            _context = context;
        }


    }
}
