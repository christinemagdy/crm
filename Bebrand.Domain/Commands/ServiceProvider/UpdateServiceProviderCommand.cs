using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.ServiceProvider
{
    public class UpdateServiceProviderCommand : ServiceProviderCommand
    {
        public UpdateServiceProviderCommand(Guid id, Guid serviceid, Guid clientid)
        {
            Id = id;
            ServiceId = serviceid;
            ClientID = clientid;
        }
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
