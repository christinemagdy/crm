using Bebrand.Domain.Validations.Hr;
using System;
using Bebrand.Domain.Commands.Validations;

namespace Bebrand.Domain.Commands.Hr
{
    public class UpdateHrCommand : HrCommand
    {
        public UpdateHrCommand(Guid id, string fname, string lname, string email, DateTime birthDate)
        {
            Id = id;
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateHrCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}