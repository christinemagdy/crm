using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bebrand.Application.EventSourcedNormalizers;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Domain.Commands;
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
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        private readonly IMediatorHandler _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  IMediatorHandler mediator,
                                  UserManager<ApplicationUser> userManager,
                                  RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _mediator = mediator;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task<QueryResultResource<CustomerViewModel>> GetAll(Status? status)
        {

            if (status != null && status.Value == Status.Active)
            {
                var result = await _customerRepository.GetAllActive();
                return _mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Deactivate)
            {
                var result = await _customerRepository.GetAllDeleted();
                return _mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerViewModel>>(result);
            }
            else if (status != null && status.Value == Status.Updated)
            {
                var result = await _customerRepository.GetAvtiveUpdated();
                return _mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerViewModel>>(result);
            }

            var All = await _customerRepository.GetAll();
            return _mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerViewModel>>(All);
        }


        public async Task<QueryResultResource<CustomerViewModel>> GetAllByUser()
        {

            var result = await _customerRepository.GetAllActiveByUser();
            return _mapper.Map<QueryResult<Customer>, QueryResultResource<CustomerViewModel>>(result);

        }

        public async Task<CustomerViewModel> GetById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(await _customerRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(CreateCustomerViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
            var Registered = await _mediator.SendCommand(registerCommand);
            if (Registered.IsValid)
            {
                var user = new ApplicationUser { UserName = customerViewModel.Email, Email = customerViewModel.Email, Status = Status.Active, ParentUserId = registerCommand.Id };
                var result = await _userManager.CreateAsync(user, customerViewModel.Password);
                if (result.Succeeded)
                {
                    var Role = _roleManager.Roles.FirstOrDefault(x => x.Name == Roles.Salesdirector.ToString());
                    if (Role != null)
                    {
                        await _userManager.AddToRoleAsync(user, Role.Name);
                    }
                }
            }
            return Registered;
        }

        public async Task<ValidationResult> Update(UpdateCustomerViewModel customerViewModel)
        {
            List<ValidationFailure> ValidationFailure = new List<ValidationFailure>();
            try
            {
                var UpdateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);

                ApplicationUser userToVerify = _userManager.Users.FirstOrDefault(x => x.ParentUserId == customerViewModel.Id);
                userToVerify.Email = customerViewModel.Email;
                userToVerify.UserName  = customerViewModel.Email;
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
            var removeCommand = new RemoveCustomerCommand(id, status);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<ValidationResult> UserStatus(Guid id, Status status)
        {

            List<ValidationFailure> ValidationFailure = new List<ValidationFailure>();
            try
            {
                var remove = new RemoveCustomerCommand(id, status);

                var Customer = await _customerRepository.GetById(id);

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

        public Task<IList<CustomerHistoryData>> GetAllHistory(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
