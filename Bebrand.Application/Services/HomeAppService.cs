using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Application.ViewModels.TeamLeaderView;
using Bebrand.Application.ViewModels.TeamMemberView;
using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Services
{
    public class HomeAppService : IHomeAppService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClientAppService _clientAppService;

        private readonly ITeamLeaderRepository _teamLeaderRepository;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly ICustomerAppService _customerApp;
        private readonly ITeamMemberAppService _teamMemberAppService;
        private readonly IUser _user;

        private readonly IMapper _mapper;

        public HomeAppService(UserManager<ApplicationUser> userManager, IClientAppService clientAppService, IMapper mapper, ICustomerAppService customerApp,
            IUser user, ITeamLeaderRepository teamLeaderRepository, ITeamMemberRepository teamMemberRepository, ITeamMemberAppService teamMemberAppService)
        {
            _userManager = userManager;
            _clientAppService = clientAppService;
            _mapper = mapper;

            _customerApp = customerApp;
            _user = user;
            _teamLeaderRepository = teamLeaderRepository;
            _teamMemberRepository = teamMemberRepository;
            _teamMemberAppService = teamMemberAppService;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        // Get Client Data without Pagination
        public async Task<QueryMultipleResult<PersonalHome>> Home()
        {
            var data = await _userManager.FindByIdAsync(_user.GetUserId());
            var result = _mapper.Map<PersonalHome>(data);
            //var client = _clientAppService.GetByUser(UserStatus.Active, null, new OwnerParameters());
            //result.ClientViews.AddRange(client.data.ToList());

            //var salesDirector = await _customerApp.GetAllByUser();
            var teamLeader = _teamLeaderRepository.GetBySlaesDirectorId(Guid.Parse(_user.GetParentUserId()));
            var teamMember = _teamMemberRepository.GetByTeamLeaderId(Guid.Parse(_user.GetParentUserId()));

            #region  If Parent equals sales director

            if (teamLeader.Data.Count() != 0)
            {
                result.teamLeaderViewModels.AddRange(_mapper.Map<IEnumerable<TeamLeaderViewModel>>(teamLeader.Data.ToList()));
                var getTeamMember = await _teamMemberRepository.GetAllActivePerUser();
                result.teamMemberViewModels.AddRange(_mapper.Map<IEnumerable<TeamMemberViewModel>>(getTeamMember.data));
                var salesDirectorDetails = await _customerApp.GetById(Guid.Parse(_user.GetParentUserId()));
                result.BirthDate = salesDirectorDetails.BirthDate;
                result.Fname = salesDirectorDetails.FName;
                result.Lname = salesDirectorDetails.LName;
                return new QueryMultipleResult<PersonalHome>(result);
            }
            #endregion


            #region If Parent equals teamleader

            if (teamMember.Data.Count() != 0)
            {
                result.teamMemberViewModels.AddRange(_mapper.Map<IEnumerable<TeamMemberViewModel>>(teamMember.Data.ToList()));
                var salesDirector = _teamLeaderRepository.GetSlaesDirectorById(Guid.Parse(_user.GetParentUserId())).Data.ToList();
                result.SalesDirector.AddRange(_mapper.Map<IEnumerable<CustomerViewModel>>(salesDirector));
                var teamLeaderDetails = await _teamLeaderRepository.GetById(Guid.Parse(_user.GetParentUserId()));
                result.BirthDate = teamLeaderDetails.BirthDate;
                result.Fname = teamLeaderDetails.FName;
                result.Lname = teamLeaderDetails.LName;
                return new QueryMultipleResult<PersonalHome>(result);
            }

            #endregion

            #region If Parent equals teamMember

            var teamLeaders = _teamMemberRepository.GetTeamLeaderById(Guid.Parse(_user.GetParentUserId()));
            result.teamLeaderViewModels.AddRange(_mapper.Map<IEnumerable<TeamLeaderViewModel>>(teamLeaders.Data.ToList()));
            var salaesDirector = _teamLeaderRepository.GetSlaesDirectorById(teamLeaders.Data.FirstOrDefault().Id);
            result.SalesDirector.AddRange(_mapper.Map<IEnumerable<CustomerViewModel>>(salaesDirector.Data.ToList()));
            var details = await _teamMemberAppService.GetById(Guid.Parse(_user.GetParentUserId()));
            result.BirthDate = details.BirthDate;
            result.Fname = details.FName;
            result.Lname = details.LName;
            return new QueryMultipleResult<PersonalHome>(result);

            #endregion

            //return result;
        }
    }
}
