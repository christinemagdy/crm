using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bebrand.Infra.Data.Repository
{
    public class VacanciesMailRepository : GenericRepository<VacanciesMail>, IVacanciesMailRepository
    {
        private readonly BebrandContext _context;
        public VacanciesMailRepository(BebrandContext context, IUser user) : base(context, user)
        {
            this._context = context;
        }
        public IQueryable<string> UniqueIds()
        {
            return _context.VacanciesMails.Select(x => x.UniqueIds);
        }
    }
}
