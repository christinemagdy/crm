using System;
using Bebrand.Domain.Commands.Validations;
using Bebrand.Domain.Validations;

namespace Bebrand.Domain.Commands
{
    public class RegisterNewCustomerCommand : CustomerCommand
    {
        public RegisterNewCustomerCommand(string fname, string lname, string email, DateTime birthDate)
        {
            FName = fname;
            LName = lname;
            Email = email;
            BirthDate = birthDate;
            Id = Guid.NewGuid();
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}