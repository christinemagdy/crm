using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Area
{
    public class RemoveAreaCommand : AreaCommand
    {
        public RemoveAreaCommand(Guid id)
        {
            Id = id;
            AggregateId = id;

        }
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
