using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bebrand.Application.EventSourcedNormalizers;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Application.ViewModels.HrView;
using Bebrand.Domain.Commands;
using Bebrand.Domain.Commands.Hr;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Bebrand.Infra.CrossCutting.Identity.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Mediator;


namespace Bebrand.Application.Services
{
    public class HrAppService : IHrAppService
    {
        private readonly IMapper _mapper;
        private readonly IHrRepository _hrRepository;

        private readonly IMediatorHandler _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public HrAppService(IMapper mapper,
                                  IHrRepository hrRepository,
                                  IMediatorHandler mediator,
                                  UserManager<ApplicationUser> userManager,
                                  RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _hrRepository = hrRepository;
            _mediator = mediator;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task<QueryResultResource<HrViewModel>> GetAll(Status? status)
        {

            if (status != null && status.Value == Status.Active)
            {
                var result = await _hrRepository.GetAllActive();
                return _mapper.Map<QueryResult<Hr>, QueryResultResource<HrViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Deactivate)
            {
                var result = await _hrRepository.GetAllDeleted();
                return _mapper.Map<QueryResult<Hr>, QueryResultResource<HrViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Updated)
            {
                var result = await _hrRepository.GetAvtiveUpdated();
                return _mapper.Map<QueryResult<Hr>, QueryResultResource<HrViewModel>>(result);
            }

            var All = await _hrRepository.GetAll();
            return _mapper.Map<QueryResult<Hr>, QueryResultResource<HrViewModel>>(All);
        }


        public async Task<QueryResultResource<HrViewModel>> GetAllByUser()
        {

            var result = await _hrRepository.GetAllActiveByUser();
            return _mapper.Map<QueryResult<Hr>, QueryResultResource<HrViewModel>>(result);

        }

        public async Task<HrViewModel> GetById(Guid id)
        {
            return _mapper.Map<HrViewModel>(await _hrRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(CreateHrViewModel hrViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewHrCommand>(hrViewModel);
            var Registered = await _mediator.SendCommand(registerCommand);
            if (Registered.IsValid)
            {
                var user = new ApplicationUser { UserName = hrViewModel.Email, Email = hrViewModel.Email, Status = Status.Active, ParentUserId = registerCommand.Id };
                var result = await _userManager.CreateAsync(user, hrViewModel.Password);
                if (result.Succeeded)
                {
                    var Role = _roleManager.Roles.FirstOrDefault(x => x.Name == Roles.Hr.ToString());
                    if (Role != null)
                    {
                        await _userManager.AddToRoleAsync(user, Role.Name);
                    }
                }
            }
            return Registered;
        }

        public async Task<ValidationResult> Update(UpdateHrViewModel hrViewModel)
        {
            List<ValidationFailure> ValidationFailure = new List<ValidationFailure>();
            try
            {
                var UpdateCommand = _mapper.Map<UpdateHrCommand>(hrViewModel);

                ApplicationUser userToVerify = _userManager.Users.FirstOrDefault(x => x.ParentUserId == hrViewModel.Id);
                userToVerify.Email = hrViewModel.Email;
                userToVerify.UserName = hrViewModel.Email;
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

        public async Task<ValidationResult> Remove(Guid id, Status status)
        {
            var removeCommand = new RemoveHrCommand(id, status);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<ValidationResult> UserStatus(Guid id, Status status)
        {

            List<ValidationFailure> ValidationFailure = new List<ValidationFailure>();
            try
            {
                var remove = new RemoveHrCommand(id, status);

                var Customer = await _hrRepository.GetById(id);

                var User = await _userManager.Users.FirstOrDefaultAsync(x => x.ParentUserId == Customer.Id);

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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
