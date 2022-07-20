using Bebrand.Application.Interfaces;
using Bebrand.Application.Services;
using Bebrand.Application.VacanciesMails;
using Bebrand.Domain;
using Bebrand.Domain.CommandHandlers;
using Bebrand.Domain.Commands;
using Bebrand.Domain.Commands.Area;
using Bebrand.Domain.Commands.Client;
using Bebrand.Domain.Commands.Hr;
using Bebrand.Domain.Commands.Jobs;
using Bebrand.Domain.Commands.Service;
using Bebrand.Domain.Commands.ServiceProvider;
using Bebrand.Domain.Commands.TeamLeader;
using Bebrand.Domain.Commands.TeamMember;
using Bebrand.Domain.Commands.VacanciesMail;
using Bebrand.Domain.EventHandlers;
using Bebrand.Domain.Events;
using Bebrand.Domain.Events.Client;
using Bebrand.Domain.Events.Hr;
using Bebrand.Domain.Events.TeamLeader;
using Bebrand.Domain.Events.TeamMember;
using Bebrand.Domain.Interfaces;
using Bebrand.Infra.CrossCutting.Bus;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Bebrand.Infra.Data;
using Bebrand.Infra.Data.Repository;
using Elite.Domain.CommandHandlers;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IAreaAppService, AreaAppService>();
            services.AddScoped<IServicesAppService, ServicesAppService>();
            services.AddScoped<IServiceProviderAppService, ServiceProviderAppService>();
            services.AddScoped<ITeamLeaderAppService, TeamLeaderAppService>();
            services.AddScoped<IClientAppService, ClientAppService>();
            services.AddScoped<ITeamMemberAppService, TeamMemberAppService>();
            services.AddScoped<IHomeAppService, HomeAppService>();
            services.AddScoped<ImailAppService, MailAppService>();
            services.AddScoped<IJobAppservice, JobAppservice>();
            services.AddScoped<IVacanciesMailAppService, VacanciesMailAppService>();
            services.AddScoped<IFromFileAppService, FromFileAppService>();
            services.AddScoped<IHrAppService, HrAppService>();


            // Domain - Events
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<TeamLeaderRegisteredEvent>, TeamLeaderEventHandler>();
            services.AddScoped<INotificationHandler<TeamLeaderUpdatedEvent>, TeamLeaderEventHandler>();
            services.AddScoped<INotificationHandler<TeamLeaderRemovedEvent>, TeamLeaderEventHandler>();
            services.AddScoped<INotificationHandler<ClientRegisterdEvent>, ClientEventHandler>();
            services.AddScoped<INotificationHandler<ClientUpdatedEvent>, ClientEventHandler>();
            services.AddScoped<INotificationHandler<ClientRemovedEvent>, ClientEventHandler>();
            services.AddScoped<INotificationHandler<TeamMemberRegisteredEvent>, TeamMemberEventHandler>();
            services.AddScoped<INotificationHandler<TeamMemberUpdatedEvent>, TeamMemberEventHandler>();
            services.AddScoped<INotificationHandler<TeamMemberRemovedEvent>, TeamMemberEventHandler>();

            services.AddScoped<INotificationHandler<HrRegisteredEvent>, HrEventHandler>();
            services.AddScoped<INotificationHandler<HrUpdatedEvent>, HrEventHandler>();
            services.AddScoped<INotificationHandler<HrRemovedEvent>, HrEventHandler>();


            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, ValidationResult>, CustomerCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterNewAreaCommand, ValidationResult>, AreaCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAreaCommand, ValidationResult>, AreaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveAreaCommand, ValidationResult>, AreaCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterNewServiceCommand, ValidationResult>, ServiceCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateServiceCommand, ValidationResult>, ServiceCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveServiceCommand, ValidationResult>, ServiceCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterNewServiceProviderCommand, ValidationResult>, ServiceProviderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateServiceProviderCommand, ValidationResult>, ServiceProviderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveServiceProviderCommand, ValidationResult>, ServiceProviderCommandHandler>();


            services.AddScoped<IRequestHandler<RegisterTeamLeaderCommand, ValidationResult>, TeamLeaderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTeamLeaderCommand, ValidationResult>, TeamLeaderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveTeamLeaderCommand, ValidationResult>, TeamLeaderCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterNewClientCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateClientCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveClientCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<HardDeleteClientCommand, ValidationResult>, ClientCommandHandler>();


            services.AddScoped<IRequestHandler<RegisterTeamMemberCommand, ValidationResult>, TeamMemberCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTeamMemberCommand, ValidationResult>, TeamMemberCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveTeamMemberCommand, ValidationResult>, TeamMemberCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterNewJobsCommand, ValidationResult>, JobsCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateJobsCommand, ValidationResult>, JobsCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveJobsCommand, ValidationResult>, JobsCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterVacanciesMailMemberCommand, ValidationResult>, VacanciesMailCommandHandler>();


            services.AddScoped<IRequestHandler<RegisterNewHrCommand, ValidationResult>, HrCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateHrCommand, ValidationResult>, HrCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveHrCommand, ValidationResult>, HrCommandHandler>();


            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IAreaRepository, AreaRepository>();

            services.AddScoped<IServiceRepository, ServiceRepository>();

            services.AddScoped<IServiceProviderRepository, ServiceProviderRepository>();

            services.AddScoped<ITeamLeaderRepository, TeamLeaderRepository>();

            services.AddScoped<IHrRepository, HrRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IJobsRepository, JobsRepository>();
            services.AddScoped<IVacanciesMailRepository, VacanciesMailRepository>();
            services.AddScoped<BebrandContext>();



            /*------------------------- Infra - Identity ---------------------- */
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
