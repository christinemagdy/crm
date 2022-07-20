using Bebrand.Application.ViewModels;
using Bebrand.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface IHomeAppService : IDisposable
    {
        Task<QueryMultipleResult<PersonalHome>> Home();
    }
}
