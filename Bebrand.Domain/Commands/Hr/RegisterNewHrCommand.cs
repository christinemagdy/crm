using Bebrand.Domain.Validations.Hr;
using System;
using Bebrand.Domain.Commands.Validations;

namespace Bebrand.Domain.Commands.Hr
{
    public class RegisterNewHrCommand : HrCommand
    {
        public RegisterNewHrCommand(string fname, string lname, string email, DateTime birthDate)
        {
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
            Id = Guid.NewGuid();
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewHrCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}