using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.Services;
using Bebrand.Domain.Commands.Service;
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
    public class ServicesAppService : IServicesAppService
    {
        protected IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public ServicesAppService(IServiceRepository serviceRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<QueryMultipleResult<IEnumerable<ServiceViewModel>>> GetAll()
        {
            var Data = await _serviceRepository.Get();
            return _mapper.Map<QueryMultipleResult<IEnumerable<ServiceViewModel>>>(Data);
        }

        public async Task<QueryMultipleResult<ServiceViewModel>> GetById(Guid id)
        {
            var Data = await _serviceRepository.GetById(id);
            return _mapper.Map<QueryMultipleResult<ServiceViewModel>>(Data);
        }

        public async Task<ValidationResult> Register(CreateServiceViewModel ServiceViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewServiceCommand>(ServiceViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveServiceCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }
        public async Task<ValidationResult> Update(UpdateServiceViewModel ServiceViewModel)
        {
            var updateCommand = _mapper.Map<UpdateServiceCommand>(ServiceViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
