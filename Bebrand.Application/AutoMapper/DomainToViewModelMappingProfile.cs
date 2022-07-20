using AutoMapper;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.AreaView;
using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Application.ViewModels.HrView;
using Bebrand.Application.ViewModels.JobsView;
using Bebrand.Application.ViewModels.ServiceProvider;
using Bebrand.Application.ViewModels.Services;
using Bebrand.Application.ViewModels.TeamLeaderView;
using Bebrand.Application.ViewModels.TeamMemberView;
using Bebrand.Application.ViewModels.VacanciesMail;
using Bebrand.Domain.Models;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bebrand.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap(typeof(QueryMultipleResult<>), typeof(QueryMultipleResult<>));
            CreateMap<Customer, CustomerViewModel>().ReverseMap();

            //Area
            CreateMap<Area, AreaViewModel>().ReverseMap();
            CreateMap<Area, UpdateAreaViewModel>().ReverseMap();
            CreateMap<Area, CreateAreaViewModel>().ReverseMap();

            //Service
            CreateMap<Service, ServiceViewModel>().ReverseMap();
            CreateMap<Service, UpdateServiceViewModel>().ReverseMap();
            CreateMap<Service, CreateServiceViewModel>().ReverseMap();

            //ServiceProvider
            CreateMap<ServiceProvider, ServiceProviderViewModel>().ReverseMap();
            CreateMap<ServiceProvider, UpdateServiceProviderViewModel>().ReverseMap();
            CreateMap<ServiceProvider, CreateServiceProviderViewModel>().ReverseMap();

            //TeamLeader
            CreateMap<TeamLeader, TeamLeaderViewModel>().ReverseMap();
            CreateMap<TeamLeader, UpdateTeamLeaderViewModel>().ReverseMap();
            CreateMap<TeamLeader, CreateTeamLeaderViewModel>().ReverseMap();

            //Hr
            CreateMap<Hr, HrViewModel>().ReverseMap();
            CreateMap<Hr, UpdateHrViewModel>().ReverseMap();
            CreateMap<Hr, CreateHrViewModel>().ReverseMap();

            //Client
            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<Client, UpdateClientViewModel>().ReverseMap();
            CreateMap<Client, CreateClientViewModel>().ReverseMap();

            //TeamMember
            CreateMap<TeamMember, TeamMemberViewModel>().ReverseMap();
            CreateMap<TeamMember, UpdateTeamMemberViewModel>().ReverseMap();
            CreateMap<TeamMember, CreateTeamMemberViewModel>().ReverseMap();

            //Jobs
            CreateMap<Jobs, JobsViewModel>().ReverseMap();
            CreateMap<CreateJobsViewModel, Jobs>().ReverseMap();

            //ApplicationUser
            CreateMap<ApplicationUser, PersonalHome>().ReverseMap();

            //VacanciesMail
            CreateMap<VacanciesMail, VacanciesMailViewModel>().ReverseMap();
            CreateMap<VacanciesMail, CreateVacanciesMailViewModel>().ReverseMap();


        }
    }
}
