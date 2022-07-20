using Bebrand.Domain.Validations.TeamLeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.TeamLeader
{
    public class UpdateTeamLeaderCommand : TeamLeaderCommand
    {
        public UpdateTeamLeaderCommand(Guid id, string fname, string lname, string email, DateTime birthDate)
        {
            Id = id;
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateTeamLeaderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
