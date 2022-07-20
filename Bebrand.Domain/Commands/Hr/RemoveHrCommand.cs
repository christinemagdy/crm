using Bebrand.Domain.Validations.Hr;
using System;
using Bebrand.Domain.Commands.Validations;
using Bebrand.Domain.Enums;

namespace Bebrand.Domain.Commands.Hr
{
    public class RemoveHrCommand : HrCommand
    {
        public RemoveHrCommand(Guid id, Status status)
        {
            Id = id;
            Status = status;
        }

        //public override bool IsValid()
        //{
        //    ValidationResult = new RemoveHrCommandValidation().Validate(this);
        //    return ValidationResult.IsValid;
        //}
        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}