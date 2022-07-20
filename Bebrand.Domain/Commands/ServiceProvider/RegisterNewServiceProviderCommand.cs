using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.ServiceProvider
{
    public class RegisterNewServiceProviderCommand : ServiceProviderCommand
    {
        public RegisterNewServiceProviderCommand(Guid clientid, Guid serviceid)
        {
            Id = Guid.NewGuid();
            ClientID = clientid;
            ServiceId = serviceid;
        }
        public override bool IsValid()
        {
            return base.IsValid();
        }

    }
}
