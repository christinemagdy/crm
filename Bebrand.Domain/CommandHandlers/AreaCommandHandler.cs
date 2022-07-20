using Bebrand.Domain.Commands.Area;
using Bebrand.Domain.Interfaces;
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
using Bebrand.Domain.Models;
namespace Bebrand.Domain.CommandHandlers
{
    public class AreaCommandHandler : CommandHandler,
          IRequestHandler<RegisterNewAreaCommand, ValidationResult>,
          IRequestHandler<UpdateAreaCommand, ValidationResult>,
          IRequestHandler<RemoveAreaCommand, ValidationResult>
    {
        private readonly IAreaRepository _AreaRepository;
        private readonly IMediatorHandler Bus;

        public IUser User { get; }

        public AreaCommandHandler(IAreaRepository AreaRepository, IUser user, IMediatorHandler Bus)
        {
            this.Bus = Bus;

            _AreaRepository = AreaRepository;
            User = user;
        }

        public async Task<ValidationResult> Handle(RegisterNewAreaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;


            var Area = new Area(message.Id, message.Name, DateTime.Now, User.GetUserId());

            var existingArea = await _AreaRepository.IfAreaExist(Area.Name);

            if (existingArea)
            {
                AddError($"{message.Name} has already been taken.");
                return ValidationResult;
            }

            //await Bus.PublishEvent(new AreaRegisteredEvent(Area.Id, Area.FName, Area.LName, Area.Email, Area.BirthDate));
            _AreaRepository.Add(Area);
            return await Commit(_AreaRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateAreaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var Area = new Area(message.Id, message.Name, DateTime.Now, User.GetUserId());
            var existingArea = await _AreaRepository.IfAreaExist(Area.Name);

            if (existingArea)
            {
                AddError($"{message.Name} has already been taken.");
                return ValidationResult;
            }

            //Area.AddDomainEvent(new AreaUpdatedEvent(Area.Id, Area.Name, Area.Email, Area.BirthDate));
            //await Bus.PublishEvent(new AreaUpdatedEvent(Area.Id, Area.FName, Area.LName, Area.Email, Area.BirthDate));

            _AreaRepository.Update(Area);

            return await Commit(_AreaRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveAreaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var Area = await _AreaRepository.GetById(message.Id);

            if (Area is null)
            {
                AddError("The Area doesn't exists.");
                return ValidationResult;
            }

            //Area.AddDomainEvent(new AreaRemovedEvent(message.Id));
            //await Bus.PublishEvent(new AreaRemovedEvent(message.Id));

            _AreaRepository.Remove(Area);

            return await Commit(_AreaRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _AreaRepository.Dispose();
        }
    }
}
