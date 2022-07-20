using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Client
{
    public class RemoveClientCommand : ClientCommand
    {
        public RemoveClientCommand(Guid id, UserStatus status)
        {
            Id = id;
            AggregateId = id;
            Status = status;
        }

        public override bool IsValid()
        {
            return base.IsValid();
            //ValidationResult = new RemoveClientCommandValidation().Validate(this);
            //return ValidationResult.IsValid;
        }
    }
}
