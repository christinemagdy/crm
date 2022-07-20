using System;
using System.Threading;
using System.Threading.Tasks;
using Bebrand.Domain.Commands;
using Bebrand.Domain.Commands.Hr;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Events;
using Bebrand.Domain.Events.Hr;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Data;
using NetDevPack.Mediator;
using NetDevPack.Messaging;

namespace Bebrand.Domain.CommandHandlers
{
    public class HrCommandHandler : CommandHandler,
          IRequestHandler<RegisterNewHrCommand, ValidationResult>,
          IRequestHandler<UpdateHrCommand, ValidationResult>,
          IRequestHandler<RemoveHrCommand, ValidationResult>
    {
        private readonly IHrRepository _hrRepository;
        private readonly IMediatorHandler Bus;



        public IUser User { get; }

        public HrCommandHandler(IHrRepository hrRepository, IUser user, IMediatorHandler Bus)
        {
            this.Bus = Bus;

            _hrRepository = hrRepository;
            User = user;
        }

        public async Task<ValidationResult> Handle(RegisterNewHrCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Hr(message.Id, message.FName, message.LName, message.Email, message.BirthDate, User.GetUserId(), DateTime.Now, Status.Active);

            await Bus.PublishEvent(new HrRegisteredEvent(customer.Id, customer.FName, customer.LName, customer.Email, customer.BirthDate));
            await _hrRepository.Add(customer);
            var Validate = new ValidationResult();
            var existance = await _hrRepository.Checke(filter: x => x.Email == message.Email);
            if (existance.Data == null)
                return await Commit(_hrRepository.UnitOfWork);
            var Failure = new ValidationFailure("Email", $"{message.Email} already exist");
            Validate.Errors.Add(Failure);
            return Validate;
        }

        public async Task<ValidationResult> Handle(UpdateHrCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Hr(message.Id, message.FName, message.LName, message.Email, message.BirthDate, User.GetUserId(), DateTime.Now, Status.Active);
            var existingCustomer = await _hrRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                if (!existingCustomer.Equals(customer))
                {
                    AddError("The e-mail has already been taken.");
                    return ValidationResult;
                }
            }

            //customer.AddDomainEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
            await Bus.PublishEvent(new HrUpdatedEvent(customer.Id, customer.FName, customer.LName, customer.Email, customer.BirthDate));

            await _hrRepository.Update(customer);

            return await Commit(_hrRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveHrCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            var customer = await _hrRepository.GetById(message.Id);
            if (customer is null)
            {
                AddError("The customer doesn't exists.");
                return ValidationResult;
            }
            await Bus.PublishEvent(new CustomerRemovedEvent(message.Id));
            _hrRepository.UserStatus(message.Id, message.Status);
            return await Commit(_hrRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _hrRepository.Dispose();
        }
    }
}