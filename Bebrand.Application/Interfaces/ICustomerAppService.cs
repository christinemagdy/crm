using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bebrand.Application.EventSourcedNormalizers;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Domain.Enums;
using FluentValidation.Results;

namespace Bebrand.Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        Task<QueryResultResource<CustomerViewModel>> GetAll(Status? status);
        Task<QueryResultResource<CustomerViewModel>> GetAllByUser();
        Task<CustomerViewModel> GetById(Guid id);
        Task<ValidationResult> Register(CreateCustomerViewModel customerViewModel);
        Task<ValidationResult> Update(UpdateCustomerViewModel customerViewModel);
        Task<ValidationResult> Remove(Guid id, Status status);
        Task<ValidationResult> UserStatus(Guid id, Status status);
        Task<IList<CustomerHistoryData>> GetAllHistory(Guid id);
    }
}
