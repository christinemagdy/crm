using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.ServiceProvider
{
    public class RemoveServiceProviderCommand : ServiceProviderCommand
    {
        public RemoveServiceProviderCommand(Guid id)
        {
            Id = id;
        }
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
