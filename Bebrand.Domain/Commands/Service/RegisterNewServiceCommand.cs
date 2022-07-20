using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Service
{
    public class RegisterNewServiceCommand : ServiceCommand
    {
        public RegisterNewServiceCommand(string name)
        {
            Id = Guid.NewGuid();
            this.Name = name;
        }
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
