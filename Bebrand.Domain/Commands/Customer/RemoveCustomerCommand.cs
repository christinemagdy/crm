using System;
using Bebrand.Domain.Commands.Validations;
using Bebrand.Domain.Enums;

namespace Bebrand.Domain.Commands
{
    public class RemoveCustomerCommand : CustomerCommand
    {
        public RemoveCustomerCommand(Guid id, Status status)
        {
            Id = id;
            AggregateId = id;
            Status = status;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}