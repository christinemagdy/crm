using Bebrand.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Interfaces
{
    public interface IVacanciesMailRepository : IRepository<VacanciesMail>, IGenericRepository<VacanciesMail>
    {
        IQueryable<string> UniqueIds();
    }
}
