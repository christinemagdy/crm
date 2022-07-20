using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class TeamMember : EntityInfo, IAggregateRoot
    {
        public TeamMember(Guid id, string fname, string lname, string email, DateTime birthDate, string modifiedBy, DateTime modifiedOn, Guid teamLeaderId, Status status)
        {
            Id = id;
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
            Status = status;
            ModifiedOn = modifiedOn;
            ModifiedBy = modifiedBy;
            TeamLeaderId = teamLeaderId;

        }

        // Empty constructor for EF
        protected TeamMember() { }

        public string FName { get; private set; }

        public string LName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public Status Status { get; set; }
        public Guid TeamLeaderId { get; set; }

        public TeamLeader TeamLeader { get; set; }
    }
}
