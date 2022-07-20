using Bebrand.Domain.Core;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class ServiceProvider : EntityInfo, IAggregateRoot
    {
        public ServiceProvider(Guid id, DateTime ModifiedOn, string ModifiedBy, Guid clientID, Guid serviceId)
        {
            this.Id = id;
            this.ModifiedOn = ModifiedOn;
            this.ModifiedBy = ModifiedBy;
            ClientID = clientID;
            ServiceId = serviceId;
        }
        public ServiceProvider()
        {

        }
        public Guid ClientID { get; set; }
        public virtual Client Client { get; set; }
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
