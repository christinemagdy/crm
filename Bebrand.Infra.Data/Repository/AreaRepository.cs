using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Repository
{
    public class AreaRepository : IAreaRepository
    {
        protected readonly BebrandContext Db;
        protected readonly DbSet<Area> DbSet;
        public AreaRepository(BebrandContext context)
        {
            Db = context;
            DbSet = Db.Set<Area>();
        }
        public IUnitOfWork UnitOfWork => Db;
        public void Add(Area Area)
        {
            DbSet.Add(Area);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<QueryResult<Area>> GetAll()
        {
            var result = new QueryResult<Area>();
            var Data = await DbSet.Include(x => x.Clients).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<Area> GetById(Guid id)
        {
            return await DbSet.Include(x => x.Clients).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Area> GetByName(string Name)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task<bool> IfAreaExist(string Name)
        {
            return await DbSet.AnyAsync(x => x.Name == Name);
        }
        public void Remove(Area Area)
        {
            DbSet.Remove(Area);
        }

        public void Update(Area Area)
        {
            DbSet.Update(Area);
        }
    }
}
