using Bebrand.Domain.Commands.Jobs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Validations.Jobs
{
    public abstract class JobsValidation<T> : AbstractValidator<T> where T : JobsCommand
    {
        protected void Id()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }

    }
}
