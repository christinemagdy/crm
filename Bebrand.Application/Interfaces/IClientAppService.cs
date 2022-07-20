using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Domain.Core;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface IClientAppService : IDisposable
    {
        QueryResultResource<ClientViewModel> GetAll(UserStatus? status, OwnerParameters ownerParameters , string key = null);
        QueryResultResource<ClientViewModel> GetByUser(UserStatus? status, string key, OwnerParameters ownerParameters );
        Task<ClientViewModel> GetById(Guid id);
        Task<ValidationResult> Register(CreateClientViewModel ClientViewModel);
        Task<ValidationResult> Update(UpdateClientViewModel ClientViewModel);
        Task<ValidationResult> UserStatus(Guid id, UserStatus status);
        Task<ValidationResult> Remove(Guid id);
        Task<QueryMultipleResult<IEnumerable<ClientViewModel>>> GetForEachTeam(OwnerParameters ownerParameters, string key);
    }
}
