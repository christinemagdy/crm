using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.Data.Repository
{

    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        
        protected readonly IUser user;

        public CustomerRepository(BebrandContext context, IUser user) : base(context ,user)
        {
            this.user = user;
        }
     
        
        public async Task<Customer> GetById(Guid id)
        {
            return await DbSet.Include(x => x.TeamLeaders).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<QueryResult<Customer>> GetAllActive()
        {
            var result = new QueryResult<Customer>();
            var Data = await DbSet.
                Where(x => x.Status != Status.Deactivate).
                Include(x => x.TeamLeaders).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }
        public async Task<QueryResult<Customer>> GetAllActiveByUser()
        {
            var result = new QueryResult<Customer>();
            var Data = await DbSet.
                Where(x => x.Status != Status.Deactivate && x.ModifiedBy == user.GetUserId()).
                Include(x => x.TeamLeaders).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }
        public async Task<QueryResult<Customer>> GetAvtiveUpdated()
        {
            var result = new QueryResult<Customer>();
            var Data = await DbSet.
                Where(x => x.Status == Status.Updated).
                Include(x => x.TeamLeaders).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }

        public async Task<QueryResult<Customer>> GetAllDeleted()
        {
            var result = new QueryResult<Customer>();
            var Data = await DbSet.
                Where(x => x.Status == Status.Deactivate).
                Include(x => x.TeamLeaders).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;

        }

        public async Task<QueryResult<Customer>> GetAll()
        {
            var result = new QueryResult<Customer>();
            var Data = await DbSet.Include(x => x.TeamLeaders).ToListAsync();
            result.success = true;
            result.data = Data;
            result.Total = Data.Count;
            return result;
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public void UserStatus(Guid id, Status status)
        {
            var Details = GetById(id).Result;
            Details.Status = status;
            DbSet.Update(Details);

        }
       
        public void Remove(Customer customer)
        {
            DbSet.Remove(customer);
        }

        
    }
}
