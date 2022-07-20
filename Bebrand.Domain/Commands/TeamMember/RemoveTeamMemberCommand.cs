using Bebrand.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.TeamMember
{
    public class RemoveTeamMemberCommand : TeamMemberCommand
    {
        public RemoveTeamMemberCommand(Guid id, Status status)
        {
            Id = id;
            Status = status;
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
