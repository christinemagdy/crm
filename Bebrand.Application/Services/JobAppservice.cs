using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels.JobsView;
using Bebrand.Domain.Commands.Jobs;
using Bebrand.Domain.Core;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Mediator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Services
{
    public class JobAppservice : IJobAppservice
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        protected IJobsRepository _JobsRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly ImailAppService _imailAppService;


        public JobAppservice(IJobsRepository jobsRepository, IMapper mapper, IMediatorHandler mediator, ImailAppService imailAppService)
        {
            _JobsRepository = jobsRepository;
            _mapper = mapper;
            _mediator = mediator;
            _imailAppService = imailAppService;
        }

        public async Task<QueryMultipleResult<IEnumerable<JobsViewModel>>> GetAll(OwnerParameters parameters, UserStatus? Status)
        {
            _imailAppService.GetAllMails();
            var Data = await _JobsRepository.Get(
                filter: (x => Status == null || x.Status == Status)
                , include: x => x.Include(x => x.vacanciesMails));
            return _mapper.Map<QueryMultipleResult<IEnumerable<JobsViewModel>>>(Data);
        }

        public async Task<QueryMultipleResult<JobsViewModel>> GetById(Guid id)
        {
            var Data = await _JobsRepository.GetById(id, include: x => x.Include(x => x.vacanciesMails));
            return _mapper.Map<QueryMultipleResult<JobsViewModel>>(Data);
        }

        public async Task<ValidationResult> Register(CreateJobsViewModel ServiceViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewJobsCommand>(ServiceViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Remove(Guid id, UserStatus status)
        {
            var removeCommand = new RemoveJobsCommand(id, status);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<ValidationResult> Restore(Guid id)
        {
            var removeCommand = new RemoveJobsCommand(id, UserStatus.Updated);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<ValidationResult> Update(JobsViewModel ServiceViewModel)
        {
            var updateCommand = _mapper.Map<UpdateJobsCommand>(ServiceViewModel);
            return await _mediator.SendCommand(updateCommand);
        }
    }
}
