using Bebrand.Domain.Enums;
using Bebrand.Domain.Validations.TeamLeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.TeamLeader
{
    public class RemoveTeamLeaderCommand : TeamLeaderCommand
    {
        public RemoveTeamLeaderCommand(Guid id , Status status)
        {
            Id = id;
            Status = status;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoveTeamLeaderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
