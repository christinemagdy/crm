using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.TeamMemberView;
using Bebrand.Domain.Enums;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface ITeamMemberAppService : IDisposable
    {
        Task<QueryResultResource<TeamMemberViewModel>> GetAll(Status? status);
        Task<TeamMemberViewModel> GetById(Guid id);
        Task<QueryResultResource<TeamMemberViewModel>> GetAllPerUser();
        Task<ValidationResult> Register(CreateTeamMemberViewModel TeamMemberViewModel);
        Task<ValidationResult> Update(UpdateTeamMemberViewModel TeamMemberViewModel);
        Task<ValidationResult> UserStatus(Guid id, Status status);

    }
}
