using Bebrand.Domain.Interfaces;

namespace Bebrand.Domain.Commands.Validations
{
    public class RemoveCustomerCommandValidation : CustomerValidation<RemoveCustomerCommand>
    {


        public RemoveCustomerCommandValidation()
        {
            ValidateId();
        }
    }
}