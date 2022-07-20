using Bebrand.Domain.Commands.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Validations.Jobs
{
    public class RemoveJobsCommandValidation : JobsValidation<RemoveJobsCommand>
    {
        public RemoveJobsCommandValidation()
        {
            Id();
        }
    }
}
