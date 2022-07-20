using Bebrand.Domain.Commands.Client;
using Bebrand.Domain.Events.Client;
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
using System.Threading;
using System.Threading.Tasks;

namespace Bebrand.Domain.CommandHandlers
{
    public class ClientCommandHandler : CommandHandler,
         IRequestHandler<RegisterNewClientCommand, ValidationResult>,
         IRequestHandler<UpdateClientCommand, ValidationResult>,
         IRequestHandler<RemoveClientCommand, ValidationResult>,
         IRequestHandler<HardDeleteClientCommand, ValidationResult>

    {
        private readonly IMediatorHandler Bus;
        public IUser User { get; }
        private readonly IClientRepository _clientRepository;
        private readonly IServiceProviderRepository _serviceProviderRepository;
        public ClientCommandHandler(IMediatorHandler bus, IUser user, IClientRepository clientRepository, IServiceProviderRepository serviceProviderRepository)
        {
            Bus = bus;
            User = user;
            _clientRepository = clientRepository;
            _serviceProviderRepository = serviceProviderRepository;
        }

        public async Task<ValidationResult> Handle(RegisterNewClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }
            var Validate = new ValidationResult();
            Guid accountManager;
            var result = request.AccountManager == Guid.Empty ? accountManager = Guid.Parse(User.GetParentUserId()) : accountManager = request.AccountManager;

            if (_clientRepository.IfkeyExistence(request.Number, false).Result)
            {
                var Failure = new ValidationFailure("Number", $"{request.Number} already exist");
                Validate.Errors.Add(Failure);
            }

            if (_clientRepository.IfkeyExistence(request.Name_of_business, false).Result)
            {
                var Failure = new ValidationFailure("Business name", $"{request.Name_of_business} already exist");
                Validate.Errors.Add(Failure);

            }
            var Data = new Client(request.Id, request.Name_of_business, request.Email, request.Number
                , request.Nameofcontact, request.Position, request.Completeaddress, request.AriaId, request.Field
                , request.Religion, request.Facebooklink, request.Instagramlink, request.Website, request.Lastfeedback
                , request.ServiceProvded, request.Case, accountManager, User.GetUserId(), DateTime.Now, DateTime.Now
                , Core.UserStatus.Active, request.Birthday, request.Call, request.Typeclient, request.Phoneone, request.Phonetwo);

            await Bus.PublishEvent(new ClientRegisterdEvent(Data.Id, Data.Name_of_business, Data.Email, Data.Number
                , Data.Nameofcontact, Data.Position, Data.Completeaddress, Data.AriaId, Data.Field
                , Data.Religion, Data.Facebooklink, Data.Instagramlink, Data.Website, Data.Lastfeedback
                , Data.ServiceProvded, Data.Case, Data.AccountManager)); //Event
            if (!Validate.IsValid) return Validate;

            await _clientRepository.Add(Data);

            var serviceProviders = new List<ServiceProvider>();
            request.ServiceProviders.ForEach(x =>
            {
                serviceProviders.Add(new ServiceProvider()
                {
                    ClientID = Data.Id,
                    ServiceId = x.Id,
                });
            });
            await Commit(_clientRepository.UnitOfWork);

            await _serviceProviderRepository.AddBulk(serviceProviders);



            return await Commit(_clientRepository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }
            var Validate = new ValidationResult();
            Guid accountManager;
            var result = request.AccountManager == Guid.Empty ? accountManager = Guid.Parse(User.GetParentUserId()) : accountManager = request.AccountManager;
            if (_clientRepository.IfkeyExistence(request.Email, true).Result)
            {
                var Failure = new ValidationFailure("Email", $"{request.Email} already exist");

                Validate.Errors.Add(Failure);

            }

            if (_clientRepository.IfkeyExistence(request.Number, true).Result)
            {
                var Failure = new ValidationFailure("Number", $"{request.Number} already exist");

                Validate.Errors.Add(Failure);

            }

            if (_clientRepository.IfkeyExistence(request.Name_of_business, true).Result)
            {
                var Failure = new ValidationFailure("Business name", $"{request.Name_of_business} already exist");

                Validate.Errors.Add(Failure);

            }

            var Data = new Client(request.Id, request.Name_of_business, request.Email, request.Number
                , request.Nameofcontact, request.Position, request.Completeaddress, request.AriaId, request.Field
                , request.Religion, request.Facebooklink, request.Instagramlink, request.Website, request.Lastfeedback
                , request.ServiceProvded, request.Case, accountManager, User.GetUserId(), DateTime.Now, Core.UserStatus.Active
                , request.Birthday, request.Call, request.Typeclient, request.Phoneone, request.Phonetwo);

            await Bus.PublishEvent(new ClientRegisterdEvent(Data.Id, Data.Name_of_business, Data.Email, Data.Number
                , Data.Nameofcontact, Data.Position, Data.Completeaddress, Data.AriaId, Data.Field
                , Data.Religion, Data.Facebooklink, Data.Instagramlink, Data.Website, Data.Lastfeedback
                , Data.ServiceProvded, Data.Case, Data.AccountManager)); //Event

            if (!Validate.IsValid) return Validate;
            await _clientRepository.Update(Data);

            var serviceProviders = new List<ServiceProvider>();
            request.ServiceProviders.ForEach(x =>
            {
                serviceProviders.Add(new ServiceProvider()
                {
                    ClientID = Data.Id,
                    ServiceId = x.Id,
                });
            });
            await Commit(_clientRepository.UnitOfWork);
            await _serviceProviderRepository.UpdateBasedOnClient(serviceProviders, Data.Id);
            return await Commit(_clientRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var IclientRepository = await _clientRepository.GetById(request.Id);

            if (IclientRepository is null)
            {
                AddError("The Client doesn't exists.");
                return ValidationResult;
            }


            await Bus.PublishEvent(new ClientRemovedEvent(request.Id));


            _clientRepository.UserStatus(request.Id, request.Status);

            return await Commit(_clientRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(HardDeleteClientCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var IclientRepository = await _clientRepository.GetById(request.Id);

            if (IclientRepository is null)
            {
                AddError("The Client doesn't exists.");
                return ValidationResult;
            }

            await Bus.PublishEvent(new ClientRemovedEvent(request.Id));

            await _clientRepository.Remove(request.Id);

            return await Commit(_clientRepository.UnitOfWork);
        }
    }
}
