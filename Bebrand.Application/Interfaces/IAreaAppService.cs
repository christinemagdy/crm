using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.AreaView;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface IAreaAppService : IDisposable
    {
        Task<QueryResultResource<AreaViewModel>> GetAll();
        Task<AreaViewModel> GetById(Guid id);


        Task<ValidationResult> Register(CreateAreaViewModel AreaViewModel);
        Task<ValidationResult> Update(UpdateAreaViewModel AreaViewModel);
        Task<ValidationResult> Remove(Guid id);

    }
}
