using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels.VacanciesMail;
using Bebrand.Domain.Commands.VacanciesMail;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.VacanciesMails
{
    public class VacanciesMailAppService : IVacanciesMailAppService
    {
        protected IVacanciesMailRepository _VacanciesMailRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public VacanciesMailAppService(IVacanciesMailRepository VacanciesMailRepository, IMapper mapper, IMediatorHandler mediator)
        {
            _VacanciesMailRepository = VacanciesMailRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<QueryMultipleResult<IEnumerable<VacanciesMailViewModel>>> GetAll()
        {
            var Data = await _VacanciesMailRepository.Get();
            return _mapper.Map<QueryMultipleResult<IEnumerable<VacanciesMailViewModel>>>(Data);
        }

        public async Task<QueryMultipleResult<VacanciesMailViewModel>> GetById(Guid id)
        {
            var Data = await _VacanciesMailRepository.GetById(id);
            return _mapper.Map<QueryMultipleResult<VacanciesMailViewModel>>(Data);
        }

        public async Task<ValidationResult> Register(CreateVacanciesMailViewModel VacanciesMailViewModel)
        {
            var registerCommand = _mapper.Map<RegisterVacanciesMailMemberCommand>(VacanciesMailViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public void Register(List<CreateVacanciesMailViewModel> VacanciesMailViewModel)
        {
            var registerCommand = _mapper.Map<List<VacanciesMail>>(VacanciesMailViewModel);
             _VacanciesMailRepository.AddBulk(registerCommand);
            _VacanciesMailRepository.SaveChanges();
        }


        public IEnumerable<string> UniqueIds()
        {
            return _VacanciesMailRepository.UniqueIds();
        }
    }
}
