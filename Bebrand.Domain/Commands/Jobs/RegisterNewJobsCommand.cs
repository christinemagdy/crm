using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Jobs
{
    public class RegisterNewJobsCommand : JobsCommand
    {
        public RegisterNewJobsCommand(string jobsTitle, List<Models.VacanciesMail> vacanciesMails)
        {
            Id = Guid.NewGuid();
            JobsTitle = jobsTitle;
            this.vacanciesMails = vacanciesMails;
        }
        public override bool IsValid()
        {
            return true;
        }
    }
}
