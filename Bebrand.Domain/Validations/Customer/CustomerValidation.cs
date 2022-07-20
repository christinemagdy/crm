using System;
using Bebrand.Domain.Interfaces;
using FluentValidation;

namespace Bebrand.Domain.Commands.Validations
{

    public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
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