using Bebrand.Domain.Commands.Service;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bebrand.Domain.CommandHandlers
{
    public class ServiceCommandHandler : CommandHandler,
          IRequestHandler<RegisterNewServiceCommand, ValidationResult>,
          IRequestHandler<UpdateServiceCommand, ValidationResult>,
          IRequestHandler<RemoveServiceCommand, ValidationResult>
    {
        private readonly IUser _user;
        protected readonly IServiceRepository _serviceRepository;
        protected readonly IServiceProviderRepository _serviceProviderRepository;
        public ServiceCommandHandler(IUser user, IServiceRepository serviceRepository, IServiceProviderRepository serviceProviderRepository)
        {
            _user = user;
            _serviceRepository = serviceRepository;
            _serviceProviderRepository = serviceProviderRepository;
        }
        public async Task<ValidationResult> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var service = new Service(request.Id, request.Name, DateTime.Now, _user.GetUserId());
            var Validate = new ValidationResult();
            await _serviceRepository.Update(service);
            if (Validate.IsValid)
                return await Commit(_serviceRepository.UnitOfWork);
            return Validate;
        }

        public async Task<ValidationResult> Handle(RegisterNewServiceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var service = new Service(request.Id, request.Name, DateTime.Now, _user.GetUserId());
            var Validate = new ValidationResult();
            await _serviceRepository.Add(service);

            if (Validate.IsValid)
                return await Commit(_serviceRepository.UnitOfWork);
            return Validate;
        }

        public async Task<ValidationResult> Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            await _serviceRepository.Remove(request.Id);
            return await Commit(_serviceRepository.UnitOfWork);
        }
    }
}
