using Bebrand.Domain.Commands.ServiceProvider;
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
    public class ServiceProviderCommandHandler : CommandHandler,
          IRequestHandler<RegisterNewServiceProviderCommand, ValidationResult>,
          IRequestHandler<UpdateServiceProviderCommand, ValidationResult>,
          IRequestHandler<RemoveServiceProviderCommand, ValidationResult>
    {
        private readonly IUser _user;
        private readonly IServiceProviderRepository _serviceProviderRepository;
        public ServiceProviderCommandHandler(IUser user, IServiceProviderRepository serviceProviderRepository)
        {
            _user = user;
            _serviceProviderRepository = serviceProviderRepository;
        }

        public async Task<ValidationResult> Handle(RegisterNewServiceProviderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var service = new ServiceProvider(request.Id, DateTime.Now, _user.GetUserId(),request.ClientID,request.ServiceId);
            var Validate = new ValidationResult();
            await _serviceProviderRepository.Add(service);
            if (Validate.IsValid)
                return await Commit(_serviceProviderRepository.UnitOfWork);
            return Validate;
        }

        public async Task<ValidationResult> Handle(UpdateServiceProviderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var service = new ServiceProvider(request.Id, DateTime.Now, _user.GetUserId(),request.ClientID , request.ServiceId);
            var Validate = new ValidationResult();
            await _serviceProviderRepository.Update(service);
            if (Validate.IsValid)
                return await Commit(_serviceProviderRepository.UnitOfWork);
            return Validate;
        }

        public async Task<ValidationResult> Handle(RemoveServiceProviderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            await _serviceProviderRepository.Remove(request.Id);
            return await Commit(_serviceProviderRepository.UnitOfWork);
        }
    }
}
