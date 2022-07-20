using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Client
{
    public class HardDeleteClientCommand : ClientCommand
    {
        public HardDeleteClientCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            return base.IsValid();
            //ValidationResult = new RemoveClientCommandValidation().Validate(this);
            //return ValidationResult.IsValid;
        }
    }
}
