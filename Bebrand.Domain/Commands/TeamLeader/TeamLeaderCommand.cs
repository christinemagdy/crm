using Bebrand.Domain.Enums;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.TeamLeader
{
    public class TeamLeaderCommand : Command
    {
        public Guid Id { get; set; }
        public string FName { get; set; }

        public string LName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public Guid SalesDirectorId { get; set; }

        public Status Status { get; set; }

    }
}
