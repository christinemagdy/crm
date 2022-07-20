using Bebrand.Domain.Commands.TeamLeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Validations.TeamLeader
{
    public class RegisterNewTeamLeaderCommandValidation : TeamLeaderValidation<RegisterTeamLeaderCommand>
    {

        public RegisterNewTeamLeaderCommandValidation()
        {

            ValidateName();
            ValidateEmail();

        }
    }
}
