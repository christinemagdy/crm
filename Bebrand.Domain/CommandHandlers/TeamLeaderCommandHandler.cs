using Bebrand.Domain.Commands.TeamLeader;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Events.TeamLeader;
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
    public class TeamLeaderCommandHandler : CommandHandler,
         IRequestHandler<RegisterTeamLeaderCommand, ValidationResult>,
         IRequestHandler<UpdateTeamLeaderCommand, ValidationResult>,
         IRequestHandler<RemoveTeamLeaderCommand, ValidationResult>
    {
        private readonly ITeamLeaderRepository _ITeamLeaderRepository;
        private readonly IMediatorHandler Bus;
        public IUser User { get; }
        public TeamLeaderCommandHandler(ITeamLeaderRepository ITeamLeaderRepository, IMediatorHandler Bus, IUser User)
        {
            this._ITeamLeaderRepository = ITeamLeaderRepository;
            this.Bus = Bus;
            this.User = User;
        }
        public async Task<ValidationResult> Handle(RegisterTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }
            var Validate = new ValidationResult();
            var Data = new TeamLeader(request.Id, request.FName, request.LName, request.Email, request.BirthDate, User.GetUserId(), DateTime.Now, request.SalesDirectorId, Status.Active);

            await Bus.PublishEvent(new TeamLeaderRegisteredEvent(Data.Id, Data.FName, Data.LName, Data.Email, Data.BirthDate)); //Event
            await _ITeamLeaderRepository.Add(Data);

            var existance = await _ITeamLeaderRepository.Checke(filter: x => x.Email == Data.Email);
            if (existance.Data == null)
                return await Commit(_ITeamLeaderRepository.UnitOfWork);
            var Failure = new ValidationFailure("Email", $"{request.Email} already exist");
            Validate.Errors.Add(Failure);

            return Validate;
        }

        public async Task<ValidationResult> Handle(RemoveTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var ITeamLeaderRepository = await _ITeamLeaderRepository.GetById(request.Id);

            if (ITeamLeaderRepository is null)
            {
                AddError("The Team Leader doesn't exists.");
                return ValidationResult;
            }


            await Bus.PublishEvent(new TeamLeaderRemovedEvent(request.Id));


            _ITeamLeaderRepository.UserStatus(request.Id, request.Status);

            return await Commit(_ITeamLeaderRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var Validate = new ValidationResult();
            var TeamLeader = new TeamLeader(request.Id, request.FName, request.LName, request.Email, request.BirthDate, User.GetUserId(), DateTime.Now, request.SalesDirectorId, Status.Active);

            await Bus.PublishEvent(new TeamLeaderUpdatedEvent(TeamLeader.Id, TeamLeader.FName, TeamLeader.LName, TeamLeader.Email, TeamLeader.BirthDate));

            await _ITeamLeaderRepository.Update(TeamLeader);



            var existance = await _ITeamLeaderRepository.Checke(filter: x => x.Email == request.Email && x.Id != request.Id);
            if (existance.Data == null)
                return await Commit(_ITeamLeaderRepository.UnitOfWork);
            var Failure = new ValidationFailure("Email", $"{request.Email} already exist");
            Validate.Errors.Add(Failure);

            return Validate;
        }
    }
}
