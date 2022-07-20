using Bebrand.Domain.Interfaces;

namespace Bebrand.Domain.Commands.Validations
{
    public class UpdateCustomerCommandValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            ValidateId();
            ValidateName();

            ValidateEmail();
        }
    }
}