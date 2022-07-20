using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.TeamLeaderView;
using Bebrand.Domain.Commands.TeamLeader;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Bebrand.Infra.CrossCutting.Identity.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Services
{
    public class TeamLeaderAppService : ITeamLeaderAppService
    {
        private readonly IMapper _mapper;
        private readonly ITeamLeaderRepository _TeamLeaderRepository;
        private readonly IMediatorHandler _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUser _User;
        public TeamLeaderAppService(IMapper mapper, ITeamLeaderRepository teamLeaderRepository, IMediatorHandler mediator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUser user)
        {
            _mapper = mapper;
            _TeamLeaderRepository = teamLeaderRepository;
            _mediator = mediator;
            _userManager = userManager;
            _roleManager = roleManager;
            _User = user;

        }
        public async Task<QueryResultResource<TeamLeaderViewModel>> GetAll(Status? status)
        {
            if (status != null && status.Value == Status.Active)
            {
                var result = await _TeamLeaderRepository.GetAllActive();

                return _mapper.Map<QueryResult<TeamLeader>, QueryResultResource<TeamLeaderViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Deactivate)
            {
                var result = await _TeamLeaderRepository.GetAllDeleted();

                return _mapper.Map<QueryResult<TeamLeader>, QueryResultResource<TeamLeaderViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Updated)
            {
                var result = await _TeamLeaderRepository.GetAvtiveUpdated();

                return _mapper.Map<QueryResult<TeamLeader>, QueryResultResource<TeamLeaderViewModel>>(result);
            }

            var All = await _TeamLeaderRepository.GetAll();
            return _mapper.Map<QueryResult<TeamLeader>, QueryResultResource<TeamLeaderViewModel>>(All);

        }

        public async Task<QueryResultResource<TeamLeaderViewModel>> GetAllPerUser(Status? status)
        {
            if (status != null && status.Value == Status.Active)
            {
                var result = await _TeamLeaderRepository.GetAllActivePerUser();
                return _mapper.Map<QueryResult<TeamLeader>, QueryResultResource<TeamLeaderViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Deactivate)
            {
                var result = await _TeamLeaderRepository.GetAllDeleted();

                return _mapper.Map<QueryResult<TeamLeader>, QueryResultResource<TeamLeaderViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Updated)
            {
                var result = await _TeamLeaderRepository.GetAvtiveUpdated();

                return _mapper.Map<QueryResult<TeamLeader>, QueryResultResource<TeamLeaderViewModel>>(result);
            }

            var All = await _TeamLeaderRepository.GetAll();
            return _mapper.Map<QueryResult<TeamLeader>, QueryResultResource<TeamLeaderViewModel>>(All);

        }

        public async Task<TeamLeaderViewModel> GetById(Guid id)
        {
            return _mapper.Map<TeamLeaderViewModel>(await _TeamLeaderRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(CreateTeamLeaderViewModel TeamLeaderViewModel)
        {
            var registerCommand = _mapper.Map<RegisterTeamLeaderCommand>(TeamLeaderViewModel);
            var Registered = await _mediator.SendCommand(registerCommand);
            if (Registered.IsValid)
            {
                var user = new ApplicationUser { UserName = TeamLeaderViewModel.Email, Email = TeamLeaderViewModel.Email, ParentUserId = registerCommand.Id, Status = Status.Active };
                var result = await _userManager.CreateAsync(user, TeamLeaderViewModel.Password);
                if (result.Succeeded)
                {
                    var Role = _roleManager.Roles.FirstOrDefault(x => x.Name == Roles.Teamleader.ToString());
                    if (Role != null)
                    {
                        await _userManager.AddToRoleAsync(user, Role.Name);
                    }
                }
            }
            return Registered;
        }

        public async Task<ValidationResult> UserStatus(Guid id, Status status)
        {
            List<ValidationFailure> ValidationFailure = new List<ValidationFailure>();
            try
            {
                var remove = new RemoveTeamLeaderCommand(id, status);

                var TeamLeader = await _TeamLeaderRepository.GetById(id);

                var User = await _userManager.Users.FirstOrDefaultAsync(x => x.ParentUserId == TeamLeader.Id);

                switch (status)
                {
                    case Status.Deactivate:
                        User.Status = Status.Deactivate;
                        break;

                    case Status.Updated:
                        User.Status = Status.Updated;
                        break;

                    default:
                        break;
                }
                var Updated = await _userManager.UpdateAsync(User);
                if (Updated.Succeeded)
                {
                    var Data = await _mediator.SendCommand(remove);
                    foreach (var item in Data.Errors)
                    {
                        var ValidationFailureitem = new ValidationFailure(item.ErrorCode, item.ErrorMessage);
                        ValidationFailure.Add(ValidationFailureitem);
                    }
                }
            }
            catch (Exception ex)
            {
                var ValidationFailureitem = new ValidationFailure(ex.Message, ex.Message);
                ValidationFailure.Add(ValidationFailureitem);
            }
            return new ValidationResult(ValidationFailure);
        }

        public async Task<ValidationResult> Update(UpdateTeamLeaderViewModel TeamLeaderViewModel)
        {
            List<ValidationFailure> ValidationFailure = new List<ValidationFailure>();
            try
            {
                var UpdateCommand = _mapper.Map<UpdateTeamLeaderCommand>(TeamLeaderViewModel);
                ApplicationUser userToVerify = _userManager.Users.FirstOrDefault(x => x.ParentUserId == TeamLeaderViewModel.Id);
                userToVerify.Email = TeamLeaderViewModel.Email;
                userToVerify.UserName = TeamLeaderViewModel.Email;
                var Updated = await _userManager.UpdateAsync(userToVerify);
                if (Updated.Succeeded)
                    return await _mediator.SendCommand(UpdateCommand);
                foreach (var item in Updated.Errors)
                {
                    var ValidationFailureitem = new ValidationFailure(item.Code, item.Description);
                    ValidationFailure.Add(ValidationFailureitem);
                }
            }
            catch (Exception ex)
            {
                var ValidationFailureitem = new ValidationFailure(ex.Message, ex.InnerException.Message);
                ValidationFailure.Add(ValidationFailureitem);
            }
            return new ValidationResult(ValidationFailure);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
