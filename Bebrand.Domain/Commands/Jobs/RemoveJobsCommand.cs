using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Validations.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Jobs
{
    public class RemoveJobsCommand : JobsCommand
    {
        public RemoveJobsCommand(Guid id, UserStatus status)
        {
            AggregateId = id;
            Id = id;
            Status = status;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoveJobsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
