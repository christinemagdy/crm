using Bebrand.Domain.Core;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class Service : EntityInfo, IAggregateRoot
    {
        public Service(Guid id, string name, DateTime ModifiedOn, string ModifiedBy)
        {
            this.ModifiedOn = ModifiedOn;
            this.ModifiedBy = ModifiedBy;
            Name = name;
            Id = id;
        }
        public Service()
        {

        }
        public string Name { get; set; }
        public ICollection<ServiceProvider> ServiceProviders { get; set; }
    }
}
