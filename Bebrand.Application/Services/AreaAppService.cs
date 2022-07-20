using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.AreaView;
using Bebrand.Domain.Commands.Area;
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
    public class AreaAppService : IAreaAppService
    {
        private readonly IMapper _mapper;
        private readonly IAreaRepository _AreaRepository;

        private readonly IMediatorHandler _mediator;
        public AreaAppService(IMapper mapper, IAreaRepository areaRepository, IMediatorHandler mediator)
        {
            _mapper = mapper;
            _AreaRepository = areaRepository;
            _mediator = mediator;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<QueryResultResource<AreaViewModel>> GetAll()
        {
            return _mapper.Map<QueryResult<Area>, QueryResultResource<AreaViewModel>>(await _AreaRepository.GetAll());
        }

        public async Task<AreaViewModel> GetById(Guid id)
        {
            return _mapper.Map<AreaViewModel>(await _AreaRepository.GetById(id));

        }

        public async Task<ValidationResult> Register(CreateAreaViewModel AreaViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewAreaCommand>(AreaViewModel);

            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var RemoveArea = _mapper.Map<RemoveAreaCommand>(await _AreaRepository.GetById(id));
            return await _mediator.SendCommand(RemoveArea);
        }

        public async Task<ValidationResult> Update(UpdateAreaViewModel AreaViewModel)
        {
            var Area = _mapper.Map<UpdateAreaCommand>(AreaViewModel);
            return await _mediator.SendCommand(Area);
        }
    }
}
