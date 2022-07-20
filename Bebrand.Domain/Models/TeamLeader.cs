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
   
    public class TeamLeader : EntityInfo, IAggregateRoot
    {
        public TeamLeader(Guid id, string fname, string lname, string email, DateTime birthDate, string modifiedBy, DateTime modifiedOn, Guid salesDirectorId, Status status)
        {
            Id = id;
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
            ModifiedBy = modifiedBy;
            Status = status;
            SalesDirectorId = salesDirectorId;
            ModifiedOn = modifiedOn;
        }

        // Empty constructor for EF
        protected TeamLeader() { }

        public string FName { get; private set; }

        public string LName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public Guid SalesDirectorId { get; set; }
        public Customer SalesDirector { get; private set; }

        public ICollection<TeamMember> TeamMembers { get; set; }
        public Status Status { get; set; }
    }
}
