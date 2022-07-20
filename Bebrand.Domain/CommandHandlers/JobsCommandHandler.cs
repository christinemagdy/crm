using Bebrand.Domain.Commands.Jobs;
using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Bebrand.Domain.CommandHandlers
{
    public class JobsCommandHandler : CommandHandler,
          IRequestHandler<RegisterNewJobsCommand, ValidationResult>,
          IRequestHandler<UpdateJobsCommand, ValidationResult>,
          IRequestHandler<RemoveJobsCommand, ValidationResult>
    {
        private readonly IJobsRepository _JobsRepository;
        private readonly IVacanciesMailRepository _vacanciesMailRepository;
        private readonly IMediatorHandler Bus;
        public IUser User { get; }

        public JobsCommandHandler(IJobsRepository JobsRepository, IUser user, IMediatorHandler Bus, IVacanciesMailRepository vacanciesMailRepository)
        {
            this.Bus = Bus;
            _JobsRepository = JobsRepository;
            User = user;
            _vacanciesMailRepository = vacanciesMailRepository;
        }

        public void Dispose()
        {
            _JobsRepository.Dispose();
        }

        public async Task<ValidationResult> Handle(RegisterNewJobsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            var Jobs = new Jobs(Guid.NewGuid(), message.JobsTitle);
            Jobs.Status = UserStatus.Active;
            await _JobsRepository.Add(Jobs);
           
            var Validate = new ValidationResult();
            if (Validate.IsValid)
                return await Commit(_JobsRepository.UnitOfWork);
            return Validate;

        }

        public async Task<ValidationResult> Handle(UpdateJobsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            var Jobs = new Jobs(message.Id, message.JobsTitle);
            Jobs.Status = UserStatus.Updated;
            await _JobsRepository.Update(Jobs);
            return await Commit(_JobsRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveJobsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var Job = await _JobsRepository.GetById(message.Id);
            if (Job is null)
            {
                AddError("The Job doesn't exists.");
                return ValidationResult;
            }
            _JobsRepository.UserStatus(message.Id, message.Status);
            return await Commit(_JobsRepository.UnitOfWork);
        }

    }
}
