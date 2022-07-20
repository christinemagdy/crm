using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.TeamLeaderView;
using Bebrand.Domain.Enums;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface ITeamLeaderAppService : IDisposable
    {
        Task<QueryResultResource<TeamLeaderViewModel>> GetAll(Status? status);
        Task<TeamLeaderViewModel> GetById(Guid id);
        Task<QueryResultResource<TeamLeaderViewModel>> GetAllPerUser(Status? status);
        Task<ValidationResult> Register(CreateTeamLeaderViewModel TeamLeaderViewModel);
        Task<ValidationResult> Update(UpdateTeamLeaderViewModel TeamLeaderViewModel);
        Task<ValidationResult> UserStatus(Guid id, Status status);

    }
}
