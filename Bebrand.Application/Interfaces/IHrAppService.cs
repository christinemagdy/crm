using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bebrand.Application.EventSourcedNormalizers;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Application.ViewModels.HrView;
using Bebrand.Domain.Enums;
using FluentValidation.Results;

namespace Bebrand.Application.Interfaces
{
    public interface IHrAppService : IDisposable
    {
        Task<QueryResultResource<HrViewModel>> GetAll(Status? status);
        Task<QueryResultResource<HrViewModel>> GetAllByUser();
        Task<HrViewModel> GetById(Guid id);
        Task<ValidationResult> Register(CreateHrViewModel hrViewModel);
        Task<ValidationResult> Update(UpdateHrViewModel hrViewModel);
        Task<ValidationResult> Remove(Guid id, Status status);
        Task<ValidationResult> UserStatus(Guid id, Status status);
    }
}
