using Bebrand.Domain.Commands.Hr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Validations.Hr
{
    public class RemoveHrCommandValidation : HrValidation<RemoveHrCommand>
    {

        public RemoveHrCommandValidation()
        {
            ValidateId();
        }
    }
}