using Bebrand.Domain.Enums;
using Bebrand.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer> , IGenericRepository<Customer>
    {
        Task<Customer> GetById(Guid id);
        Task<Customer> GetByEmail(string email);
        Task<QueryResult<Customer>> GetAll();
        Task<QueryResult<Customer>> GetAllActive();
        Task<QueryResult<Customer>> GetAllActiveByUser();
        void UserStatus(Guid id, Status status);
        Task<QueryResult<Customer>> GetAvtiveUpdated();

        Task<QueryResult<Customer>> GetAllDeleted();
     
        void Remove(Customer customer);
    }
}
