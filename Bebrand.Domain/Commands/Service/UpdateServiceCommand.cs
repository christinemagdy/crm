using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Service
{
    public class UpdateServiceCommand : ServiceCommand
    {
        public UpdateServiceCommand(Guid id , string name)
        {
            Id = id;
            Name = name;
        }
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
