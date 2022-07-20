using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.ServiceProvider
{
    public class ServiceProviderCommand : Command
    {
       
        public Guid Id { get; set; }
        public Guid ClientID { get; set; }     
        public Guid ServiceId { get; set; }

    }
}
