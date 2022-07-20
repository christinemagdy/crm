using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    [Table("SalesDirector")]
    public class Customer : EntityInfo, IAggregateRoot
    {
        public Customer(Guid id, string fname, string lname, string email, DateTime birthDate, string ModifiedBy, DateTime modifiedOn, Status status)
        {
            Id = id;
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
            Status = status;
            this.ModifiedBy = ModifiedBy;
            this.ModifiedOn = modifiedOn;
        }

        // Empty constructor for EF
        protected Customer() { }

        public string FName { get; private set; }
        public string LName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
        public Status Status { get; set; }
        public ICollection<TeamLeader> TeamLeaders { get; private set; }
    }
}
