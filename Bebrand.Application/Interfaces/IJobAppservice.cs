using Bebrand.Application.ViewModels.JobsView;
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
    public interface IJobAppservice : IDisposable
    {
        Task<QueryMultipleResult<IEnumerable<JobsViewModel>>> GetAll(OwnerParameters parameters, UserStatus? Status);
        Task<QueryMultipleResult<JobsViewModel>> GetById(Guid id);
        Task<ValidationResult> Register(CreateJobsViewModel ServiceViewModel);
        Task<ValidationResult> Update(JobsViewModel ServiceViewModel);
        Task<ValidationResult> Remove(Guid id, UserStatus status);
        Task<ValidationResult> Restore(Guid id);
    }
}
