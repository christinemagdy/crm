using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.TeamMemberView;
using Bebrand.Domain.Commands.TeamMember;
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
    public class TeamMemberAppService : ITeamMemberAppService
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberRepository _TeamMemberRepository;
        private readonly IMediatorHandler _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public TeamMemberAppService(IMapper mapper, ITeamMemberRepository teamMemberRepository, IMediatorHandler mediator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _TeamMemberRepository = teamMemberRepository;
            _mediator = mediator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<QueryResultResource<TeamMemberViewModel>> GetAll(Status? status)
        {
            if (status != null && status.Value == Status.Active)
            {
                var result = await _TeamMemberRepository.GetAllActive();

                return _mapper.Map<QueryResult<TeamMember>, QueryResultResource<TeamMemberViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Deactivate)
            {
                var result = await _TeamMemberRepository.GetAllDeleted();

                return _mapper.Map<QueryResult<TeamMember>, QueryResultResource<TeamMemberViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Updated)
            {
                var result = await _TeamMemberRepository.GetAvtiveUpdated();

                return _mapper.Map<QueryResult<TeamMember>, QueryResultResource<TeamMemberViewModel>>(result);
            }

            var All = await _TeamMemberRepository.GetAll();

            return _mapper.Map<QueryResult<TeamMember>, QueryResultResource<TeamMemberViewModel>>(All);
        }
        public async Task<QueryResultResource<TeamMemberViewModel>> GetAllPerUser()
        {
            var result = await _TeamMemberRepository.GetAllActivePerUser();
            return _mapper.Map<QueryResult<TeamMember>, QueryResultResource<TeamMemberViewModel>>(result);
        }
        public async Task<TeamMemberViewModel> GetById(Guid id)
        {
            return _mapper.Map<TeamMemberViewModel>(await _TeamMemberRepository.GetById(id));
        }


        public async Task<ValidationResult> Register(CreateTeamMemberViewModel TeamMemberViewModel)
        {
            var registerCommand = _mapper.Map<RegisterTeamMemberCommand>(TeamMemberViewModel);
            var registered = await _mediator.SendCommand(registerCommand);
            if (registered.IsValid)
            {
                var user = new ApplicationUser { UserName = TeamMemberViewModel.Email, Email = TeamMemberViewModel.Email, Status = Status.Active, ParentUserId = registerCommand.Id };
                var result = await _userManager.CreateAsync(user, TeamMemberViewModel.Password);
                if (result.Succeeded)
                {
                    var Role = _roleManager.Roles.FirstOrDefault(x => x.Name == Roles.Teammember.ToString());
                    if (Role != null)
                    {
                        await _userManager.AddToRoleAsync(user, Role.Name);
                    }
                }
            }
            return registered;
        }
        public async Task<ValidationResult> Update(UpdateTeamMemberViewModel TeamMemberViewModel)
        {
            List<ValidationFailure> ValidationFailure = new List<ValidationFailure>();
            try
            {
                var UpdateCommand = _mapper.Map<UpdateTeamMemberCommand>(TeamMemberViewModel);

                ApplicationUser userToVerify = _userManager.Users.FirstOrDefault(x => x.ParentUserId == TeamMemberViewModel.Id);
                userToVerify.Email = TeamMemberViewModel.Email;
                userToVerify.UserName = TeamMemberViewModel.Email;
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

                var ValidationFailureitem = new ValidationFailure(ex.Message, ex.Message);
                ValidationFailure.Add(ValidationFailureitem);
            }
            return new ValidationResult(ValidationFailure);

        }

        public async Task<ValidationResult> UserStatus(Guid id, Status status)
        {
            List<ValidationFailure> ValidationFailure = new List<ValidationFailure>();
            try
            {
                var remove = new RemoveTeamMemberCommand(id, status);

                var TeamMember = await _TeamMemberRepository.GetById(id);

                var User = await _userManager.Users.FirstOrDefaultAsync(x => x.ParentUserId == TeamMember.Id);

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
    }
}
