using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels.ServiceProvider;
using Bebrand.Domain.Commands.ServiceProvider;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Services
{
    public class ServiceProviderAppService : IServiceProviderAppService
    {
        protected IServiceProviderRepository _ServiceProviderRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        public ServiceProviderAppService(IServiceProviderRepository ServiceProviderRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _ServiceProviderRepository = ServiceProviderRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<QueryMultipleResult<IEnumerable<ServiceProviderViewModel>>> GetAll()
        {
            var Data = await _ServiceProviderRepository.Get();
            return _mapper.Map<QueryMultipleResult<IEnumerable<ServiceProviderViewModel>>>(Data);
        }

        public async Task<QueryMultipleResult<ServiceProviderViewModel>> GetById(Guid id)
        {
            var Data = await _ServiceProviderRepository.GetById(id);
            return _mapper.Map<QueryMultipleResult<ServiceProviderViewModel>>(Data);
        }

        public async Task<ValidationResult> Register(CreateServiceProviderViewModel ServiceViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewServiceProviderCommand>(ServiceViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveServiceProviderCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }
        public async Task<ValidationResult> Update(UpdateServiceProviderViewModel ServiceViewModel)
        {
            var updateCommand = _mapper.Map<UpdateServiceProviderCommand>(ServiceViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
