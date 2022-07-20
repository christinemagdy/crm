using Bebrand.Domain.Commands.VacanciesMail;
using Bebrand.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bebrand.Domain.Models;

namespace Bebrand.Domain.CommandHandlers
{
    public class VacanciesMailCommandHandler : CommandHandler,
          IRequestHandler<RegisterVacanciesMailMemberCommand, ValidationResult>

    {
        private readonly IUser _user;
        protected readonly IVacanciesMailRepository _VacanciesMailRepository;
        public VacanciesMailCommandHandler(IUser user, IVacanciesMailRepository VacanciesMailRepository)
        {
            _user = user;
            _VacanciesMailRepository = VacanciesMailRepository;
        }

        public async Task<ValidationResult> Handle(RegisterVacanciesMailMemberCommand request, CancellationToken cancellationToken)
        {
            var Validate = new ValidationResult();
            if (!request.IsValid()) return request.ValidationResult;
            var VacanciesMail = new VacanciesMail(request.UniqueIds, request.Subject, request.TextBody, request.Attachement, request.JobId, _user.GetUserId(), request.Sender);
            await _VacanciesMailRepository.Add(VacanciesMail);
            if (Validate.IsValid) return await Commit(_VacanciesMailRepository.UnitOfWork);
            return Validate;
        }
    }
}
