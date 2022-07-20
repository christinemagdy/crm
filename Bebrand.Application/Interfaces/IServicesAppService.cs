using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.Services;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface IServicesAppService : IDisposable
    {
        Task<QueryMultipleResult<IEnumerable<ServiceViewModel>>> GetAll();
        Task<QueryMultipleResult<ServiceViewModel>> GetById(Guid id);
        Task<ValidationResult> Register(CreateServiceViewModel ServiceViewModel);
        Task<ValidationResult> Update(UpdateServiceViewModel ServiceViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
