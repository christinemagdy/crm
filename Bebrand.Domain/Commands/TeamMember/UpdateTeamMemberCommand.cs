using Bebrand.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.TeamMember
{
    public class UpdateTeamMemberCommand : TeamMemberCommand
    {
        public UpdateTeamMemberCommand(Guid id, string FName, string LName, string Email, DateTime BirthDate, Status Status, Guid TeamLeaderId)
        {
            this.Id = id;
            this.FName = FName;
            this.LName = LName;
            this.Email = Email;
            this.BirthDate = BirthDate;
            this.Status = Status;
            this.TeamLeaderId = TeamLeaderId;
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
