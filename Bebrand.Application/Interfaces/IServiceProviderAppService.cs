using Bebrand.Application.ViewModels.ServiceProvider;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface IServiceProviderAppService : IDisposable
    {
        Task<QueryMultipleResult<IEnumerable<ServiceProviderViewModel>>> GetAll();
        Task<QueryMultipleResult<ServiceProviderViewModel>> GetById(Guid id);
        Task<ValidationResult> Register(CreateServiceProviderViewModel ServiceProviderViewModel);
        Task<ValidationResult> Update(UpdateServiceProviderViewModel ServiceProviderViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
