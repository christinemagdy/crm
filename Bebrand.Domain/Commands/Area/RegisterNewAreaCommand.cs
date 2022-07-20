using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Area
{
    public class RegisterNewAreaCommand : AreaCommand
    {
        public RegisterNewAreaCommand(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
