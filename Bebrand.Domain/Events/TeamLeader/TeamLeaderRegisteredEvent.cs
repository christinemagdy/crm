using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Events.TeamLeader
{
    public class TeamLeaderRegisteredEvent : Event
    {
        public TeamLeaderRegisteredEvent(Guid id, string fname, string lname, string email, DateTime birthDate)
        {
            Id = id;
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string FName { get; private set; }
        public string LName { get; private set; }
    }
}
