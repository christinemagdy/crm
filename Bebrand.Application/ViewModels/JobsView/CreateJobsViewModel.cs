using Bebrand.Application.ViewModels.VacanciesMail;
using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.JobsView
{
    public class CreateJobsViewModel
    {
        public string JobsTitle { get; set; }
        public List<Domain.Models.VacanciesMail> vacanciesMails { get; set; }

    }
}
