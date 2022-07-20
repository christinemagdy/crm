using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Service
{
    public class RemoveServiceCommand : ServiceCommand
    {
        public RemoveServiceCommand(Guid id)
        {
            Id = id;
        }
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
