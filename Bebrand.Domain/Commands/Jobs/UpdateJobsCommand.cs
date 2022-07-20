using Bebrand.Domain.Models;
using Bebrand.Domain.Validations.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Jobs
{
    public class UpdateJobsCommand : JobsCommand
    {
        public UpdateJobsCommand(Guid id, string jobsTitle)
        {
            Id = id;
            JobsTitle = jobsTitle;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateJobsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }


    

}
