using Bebrand.Domain.Core;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class Area : EntityInfo, IAggregateRoot
    {
        public Area(Guid id, string name, DateTime ModifiedOn, string ModifiedBy)
        {
            this.Id = id;
            this.Name = name;
            this.ModifiedOn = ModifiedOn;
            this.ModifiedBy = ModifiedBy;
        }

        public string Name { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}
