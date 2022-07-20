using Bebrand.Domain.Commands.Hr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Validations.Hr
{
    public abstract class HrValidation<T> : AbstractValidator<T> where T : HrCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.LName)
                .NotEmpty().WithMessage("Please ensure you have entered the Last Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");

            RuleFor(c => c.FName)
                .NotEmpty().WithMessage("Please ensure you have entered the first Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }



        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }


    }
}