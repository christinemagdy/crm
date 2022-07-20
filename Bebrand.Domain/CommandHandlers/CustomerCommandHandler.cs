using System;
using System.Threading;
using System.Threading.Tasks;
using Bebrand.Domain.Commands;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Events;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Data;
using NetDevPack.Mediator;
using NetDevPack.Messaging;

namespace Elite.Domain.CommandHandlers
{
    public class CustomerCommandHandler : CommandHandler,
          IRequestHandler<RegisterNewCustomerCommand, ValidationResult>,
          IRequestHandler<UpdateCustomerCommand, ValidationResult>,
          IRequestHandler<RemoveCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler Bus;



        public IUser User { get; }

        public CustomerCommandHandler(ICustomerRepository customerRepository, IUser user, IMediatorHandler Bus)
        {
            this.Bus = Bus;

            _customerRepository = customerRepository;
            User = user;
        }

        public async Task<ValidationResult> Handle(RegisterNewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(message.Id, message.FName, message.LName, message.Email, message.BirthDate, User.GetUserId(), DateTime.Now, Status.Active);

            await Bus.PublishEvent(new CustomerRegisteredEvent(customer.Id, customer.FName, customer.LName, customer.Email, customer.BirthDate));
            await _customerRepository.Add(customer);
            var Validate = new ValidationResult();
            var existance = await _customerRepository.Checke(filter: x => x.Email == message.Email);
            if (existance.Data == null)
                return await Commit(_customerRepository.UnitOfWork);
            var Failure = new ValidationFailure("Email", $"{message.Email} already exist");
            Validate.Errors.Add(Failure);
            return Validate;
        }

        public async Task<ValidationResult> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(message.Id, message.FName, message.LName, message.Email, message.BirthDate, User.GetUserId(), DateTime.Now, Status.Active);
            var existingCustomer = await _customerRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                if (!existingCustomer.Equals(customer))
                {
                    AddError("The e-mail has already been taken.");
                    return ValidationResult;
                }
            }

            //customer.AddDomainEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
            await Bus.PublishEvent(new CustomerUpdatedEvent(customer.Id, customer.FName, customer.LName, customer.Email, customer.BirthDate));

            await _customerRepository.Update(customer);

            return await Commit(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            var customer = await _customerRepository.GetById(message.Id);
            if (customer is null)
            {
                AddError("The customer doesn't exists.");
                return ValidationResult;
            }
            await Bus.PublishEvent(new CustomerRemovedEvent(message.Id));
            _customerRepository.UserStatus(message.Id, message.Status);
            return await Commit(_customerRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}