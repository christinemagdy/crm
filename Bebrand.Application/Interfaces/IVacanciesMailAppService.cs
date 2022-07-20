using Bebrand.Application.ViewModels.VacanciesMail;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface IVacanciesMailAppService : IDisposable
    {
        IEnumerable<string> UniqueIds();
        Task<QueryMultipleResult<IEnumerable<VacanciesMailViewModel>>> GetAll();
        Task<QueryMultipleResult<VacanciesMailViewModel>> GetById(Guid id);
        Task<ValidationResult> Register(CreateVacanciesMailViewModel ServiceViewModel);
        void Register(List<CreateVacanciesMailViewModel> VacanciesMailViewModel);
    }
}
