using Bebrand.Domain.Commands.TeamMember;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Events.TeamMember;
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
    public class TeamMemberCommandHandler : CommandHandler,
        IRequestHandler<RegisterTeamMemberCommand, ValidationResult>,
        IRequestHandler<UpdateTeamMemberCommand, ValidationResult>,
        IRequestHandler<RemoveTeamMemberCommand, ValidationResult>
    {
        private readonly ITeamMemberRepository _ITeamMemberRepository;
        private readonly IMediatorHandler Bus;
        public IUser User { get; }

        public TeamMemberCommandHandler(ITeamMemberRepository iTeamMemberRepository, IMediatorHandler bus, IUser user)
        {
            _ITeamMemberRepository = iTeamMemberRepository;
            Bus = bus;
            User = user;
        }


        public async Task<ValidationResult> Handle(RegisterTeamMemberCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var Data = new TeamMember(request.Id, request.FName, request.LName, request.Email, request.BirthDate, User.GetUserId(), DateTime.Now, request.TeamLeaderId, Status.Active);

            await Bus.PublishEvent(new TeamMemberRegisteredEvent(Data.Id, Data.FName, Data.LName, Data.Email, Data.BirthDate)); //Event
            var Validate = new ValidationResult();
            await _ITeamMemberRepository.Add(Data);
            var existance = await _ITeamMemberRepository.Checke(filter: x => x.Email == request.Email);
            if (existance.Data == null)
                return await Commit(_ITeamMemberRepository.UnitOfWork);
            var Failure = new ValidationFailure("Email", $"{request.Email} already exist");
            Validate.Errors.Add(Failure);

            return Validate;
        }

        public async Task<ValidationResult> Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var TeamMember = new TeamMember(request.Id, request.FName, request.LName, request.Email, request.BirthDate, User.GetUserId(), DateTime.Now, request.TeamLeaderId, Status.Active);
            var Validate = new ValidationResult();
            await Bus.PublishEvent(new TeamMemberUpdatedEvent(TeamMember.Id, TeamMember.FName, TeamMember.LName, TeamMember.Email, TeamMember.BirthDate));

            await _ITeamMemberRepository.Update(TeamMember);


            var existance = await _ITeamMemberRepository.Checke(filter: x => x.Email == request.Email && x.Id != request.Id);
            if (existance.Data == null)
                return await Commit(_ITeamMemberRepository.UnitOfWork);
            var Failure = new ValidationFailure("Email", $"{request.Email} already exist");
            Validate.Errors.Add(Failure);

            return Validate;

        }

        public async Task<ValidationResult> Handle(RemoveTeamMemberCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var ITeamMemberRepository = await _ITeamMemberRepository.GetById(request.Id);

            if (ITeamMemberRepository is null)
            {
                AddError("The Team Leader doesn't exists.");
                return ValidationResult;
            }

            //ITeamMemberRepository.AddDomainEvent(new ITeamMemberRepositoryRemovedEvent(request.Id));
            await Bus.PublishEvent(new TeamMemberRemovedEvent(request.Id));

            _ITeamMemberRepository.UserStatus(request.Id, request.Status);

            return await Commit(_ITeamMemberRepository.UnitOfWork);
        }
    }
}
