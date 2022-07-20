using AutoMapper;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.AreaView;
using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Application.ViewModels.HrView;
using Bebrand.Application.ViewModels.JobsView;
using Bebrand.Application.ViewModels.Services;
using Bebrand.Application.ViewModels.TeamLeaderView;
using Bebrand.Application.ViewModels.TeamMemberView;
using Bebrand.Application.ViewModels.VacanciesMail;
using Bebrand.Domain.Commands;
using Bebrand.Domain.Commands.Area;
using Bebrand.Domain.Commands.Client;
using Bebrand.Domain.Commands.Hr;
using Bebrand.Domain.Commands.Jobs;
using Bebrand.Domain.Commands.Service;
using Bebrand.Domain.Commands.TeamLeader;
using Bebrand.Domain.Commands.TeamMember;
using Bebrand.Domain.Commands.VacanciesMail;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Models;
using Bebrand.Infra.CrossCutting.Identity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Bebrand.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CreateCustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.FName, c.LName, c.Email, c.BirthDate));
            CreateMap<UpdateCustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.FName, c.LName, c.Email, c.BirthDate));

            //Area
            CreateMap<CreateAreaViewModel, RegisterNewAreaCommand>()
              .ConstructUsing(c => new RegisterNewAreaCommand(c.Name));
            CreateMap<UpdateAreaViewModel, UpdateAreaCommand>()
                .ConstructUsing(c => new UpdateAreaCommand(c.Id, c.Name));
            CreateMap<RemoveAreaCommand, Area>().ReverseMap();

            //Service
            CreateMap<CreateServiceViewModel, RegisterNewServiceCommand>()
             .ConstructUsing(c => new RegisterNewServiceCommand(c.Name));
            CreateMap<UpdateServiceViewModel, UpdateServiceCommand>()
                .ConstructUsing(c => new UpdateServiceCommand(c.Id, c.Name));
            CreateMap<RemoveServiceCommand, Service>().ReverseMap();

            //TeamLeader
            CreateMap<CreateTeamLeaderViewModel, RegisterTeamLeaderCommand>()
                .ConstructUsing(c => new RegisterTeamLeaderCommand(c.FName, c.LName, c.Email, c.BirthDate, c.SalesDirectorId));
            CreateMap<UpdateTeamLeaderViewModel, UpdateTeamLeaderCommand>()
                .ConstructUsing(c => new UpdateTeamLeaderCommand(c.Id, c.FName, c.LName, c.Email, c.BirthDate));

            //Hr
            CreateMap<CreateHrViewModel, RegisterNewHrCommand>()
                .ConstructUsing(c => new RegisterNewHrCommand(c.FName, c.LName, c.Email, c.BirthDate));
            CreateMap<UpdateHrViewModel, UpdateHrCommand>()
                .ConstructUsing(c => new UpdateHrCommand(c.Id, c.FName, c.LName, c.Email, c.BirthDate));

            //Client
            CreateMap<CreateClientViewModel, RegisterNewClientCommand>()
              .ConstructUsing(c => new RegisterNewClientCommand(
                  c.Name_of_business, c.Email,
            c.Number, c.Nameofcontact, c.Position, c.Completeaddress, c.AriaId,
            c.Field, c.Religion, c.Facebooklink, c.Instagramlink, c.Website,
            c.Lastfeedback, c.ServiceProvded, c.Case, c.AccountManager, c.Call, c.Typeclient, c.Phonetwo, c.Phoneone)).ForMember(x => x.ServiceProviders, x => x.MapFrom(x => x.ServiceProviders)).ReverseMap();
            CreateMap<UpdateClientViewModel, UpdateClientCommand>()
                .ConstructUsing(c => new UpdateClientCommand(c.Id, c.Name_of_business, c.Email,
            c.Number, c.Nameofcontact, c.Position, c.Completeaddress, c.AriaId,
            c.Field, c.Religion, c.Facebooklink, c.Instagramlink, c.Website,
            c.Lastfeedback, c.ServiceProvded, c.Case, c.AccountManager, c.Call, c.Typeclient, c.Phonetwo, c.Phoneone));

            //TeamMember
            CreateMap<CreateTeamMemberViewModel, RegisterTeamMemberCommand>()
              .ConstructUsing(c => new RegisterTeamMemberCommand(c.FName, c.LName, c.Email, c.BirthDate, Status.Active, c.TeamLeaderId));
            CreateMap<UpdateTeamMemberViewModel, UpdateTeamMemberCommand>()
                .ConstructUsing(c => new UpdateTeamMemberCommand(c.Id, c.FName, c.LName, c.Email, c.BirthDate, Status.Active, c.TeamLeaderId));


            //Jobs
            CreateMap<CreateJobsViewModel, RegisterNewJobsCommand>()
              .ConstructUsing(c => new RegisterNewJobsCommand(c.JobsTitle, c.vacanciesMails));
            CreateMap<JobsViewModel, UpdateJobsCommand>()
                .ConstructUsing(c => new UpdateJobsCommand(c.Id, c.JobsTitle));


            //Mails
            CreateMap<CreateVacanciesMailViewModel, RegisterVacanciesMailMemberCommand>()
              .ConstructUsing(c => new RegisterVacanciesMailMemberCommand(c.UniqueIds, c.Subject, c.TextBody, c.Attachement, c.JobId,c.Sender));

        }
    }
}
