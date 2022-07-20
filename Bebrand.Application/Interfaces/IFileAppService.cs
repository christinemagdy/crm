using Bebrand.Application.ViewModels.VacanciesMail;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface IFileAppService : IDisposable
    {
        List<CreateVacanciesMailViewModel> Save(List<MimeEntity> attachements);

    }
}
