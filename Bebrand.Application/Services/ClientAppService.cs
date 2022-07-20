using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Domain.Commands.Client;
using Bebrand.Domain.Core;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Bebrand.Infra.CrossCutting.Identity.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bebrand.Application.Services
{
    public class ClientAppService : IClientAppService
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _ClientRepository;
        private readonly IMediatorHandler _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        public ClientAppService(IMapper mapper, IClientRepository clientRepository, IMediatorHandler mediator, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _ClientRepository = clientRepository;
            _mediator = mediator;
            _userManager = userManager;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public QueryResultResource<ClientViewModel> GetAll(UserStatus? status, OwnerParameters ownerParameters = null, string key = null)
        {

            if (status != null && status.Value == Domain.Core.UserStatus.Active)
            {
                var result = _ClientRepository.GetAllActive(false, ownerParameters, key);
                var dataActive = _mapper.Map<QueryResult<Client>, QueryResultResource<ClientViewModel>>(result);

                dataActive.data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);
                return dataActive;
            }
            else if (status != null && status.Value == Domain.Core.UserStatus.Deactivate)
            {
                var result = _ClientRepository.GetAllDeleted(false, ownerParameters, key);
                var dataDeleted = _mapper.Map<QueryResult<Client>, QueryResultResource<ClientViewModel>>(result);
                dataDeleted.data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);
                return dataDeleted;
            }
            else if (status != null && status.Value == Domain.Core.UserStatus.Updated)
            {
                var result = _ClientRepository.GetAvtiveUpdated(false, ownerParameters, key);

                var dataActiveUpdated = _mapper.Map<QueryResult<Client>, QueryResultResource<ClientViewModel>>(result);
                dataActiveUpdated.data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);

                return dataActiveUpdated;
            }

            var All = _ClientRepository.GetAll(false, ownerParameters, key);

            var data = _mapper.Map<QueryResult<Client>, QueryResultResource<ClientViewModel>>(All);
            data.data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);

            return data;
        }

        public async Task<QueryMultipleResult<IEnumerable<ClientViewModel>>> GetForEachTeam(OwnerParameters ownerParameters, string key = null)
        {
            var result = _mapper.Map<QueryMultipleResult<IEnumerable<ClientViewModel>>>(await _ClientRepository.ClientsPerTeam(ownerParameters, key));
            result.Data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);
            return result;
        }
        public QueryResultResource<ClientViewModel> GetByUser(UserStatus? status, string key, OwnerParameters ownerParameters)
        {
            if (status != null && status.Value == Domain.Core.UserStatus.Active)
            {
                var result = _ClientRepository.GetAllActive(true, ownerParameters, key);
                var dataActive = _mapper.Map<QueryResult<Client>, QueryResultResource<ClientViewModel>>(result);
                dataActive.data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);
                return dataActive;
            }
            else if (status != null && status.Value == Domain.Core.UserStatus.Deactivate)
            {
                var result = _ClientRepository.GetAllDeleted(true, ownerParameters, key);
                var dataDeactive = _mapper.Map<QueryResult<Client>, QueryResultResource<ClientViewModel>>(result);
                dataDeactive.data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);

                return dataDeactive;
            }
            else if (status != null && status.Value == Domain.Core.UserStatus.Updated)
            {
                var result = _ClientRepository.GetAvtiveUpdated(true, ownerParameters, key);

                var dataUpdated = _mapper.Map<QueryResult<Client>, QueryResultResource<ClientViewModel>>(result);
                dataUpdated.data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);
                return dataUpdated;
            }
            var All = _ClientRepository.GetAll(true, ownerParameters, key);
            var data = _mapper.Map<QueryResult<Client>, QueryResultResource<ClientViewModel>>(All);
            data.data.ToList().ForEach(x => x.AccountName = _userManager.Users.FirstOrDefault(a => a.ParentUserId == x.AccountManager).Email);
            return data;
        }

        public async Task<ClientViewModel> GetById(Guid id)
        {

            return _mapper.Map<ClientViewModel>(await _ClientRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(CreateClientViewModel ClientViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewClientCommand>(ClientViewModel);

            return await _mediator.SendCommand(registerCommand);
        }

        public Task<ValidationResult> Update(UpdateClientViewModel ClientViewModel)
        {
            var UpdateCommand = _mapper.Map<UpdateClientCommand>(ClientViewModel);
            return _mediator.SendCommand(UpdateCommand);
        }
        public async Task<ValidationResult> UserStatus(Guid id, UserStatus status)
        {
            var remove = new RemoveClientCommand(id, status);
            return await _mediator.SendCommand(remove);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var remove = new HardDeleteClientCommand(id);
            return await _mediator.SendCommand(remove);
        }
    }
}
