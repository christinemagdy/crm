using Bebrand.Domain.Validations.TeamLeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.TeamLeader
{
    public class RegisterTeamLeaderCommand : TeamLeaderCommand
    {
        public RegisterTeamLeaderCommand(string fname, string lname, string email, DateTime birthDate, Guid salesdirectorid)
        {
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
            SalesDirectorId = salesdirectorid;
            Id = Guid.NewGuid();
        }
        public override bool IsValid()
        {
            ValidationResult = new RegisterNewTeamLeaderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
