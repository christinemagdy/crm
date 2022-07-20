using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels;
using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Application.ViewModels.ServiceProvider;
using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Bebrand.Infra.CrossCutting.Identity.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Services
{
    public class FromFileAppService : IFromFileAppService
    {
        private readonly IAreaRepository _AreaRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly ITeamLeaderRepository _teamLeaderRepository;
        private readonly IMapper _map;
        private readonly IUser user;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProviderRepository _serviceProviderRepository;
        private readonly IServiceRepository _serviceRepository;
        public FromFileAppService(IAreaRepository AreaRepository, IClientRepository clientRepository, IMapper map, IUser user, ICustomerRepository customerRepository, ITeamMemberRepository teamMemberRepository, ITeamLeaderRepository teamLeaderRepository, UserManager<ApplicationUser> userManager, IServiceProviderRepository serviceProviderRepository, IServiceRepository serviceRepository)
        {
            _AreaRepository = AreaRepository;
            _clientRepository = clientRepository;
            _map = map;
            this.user = user;
            _customerRepository = customerRepository;
            _teamMemberRepository = teamMemberRepository;
            _teamLeaderRepository = teamLeaderRepository;
            _userManager = userManager;
            _serviceProviderRepository = serviceProviderRepository;
            _serviceRepository = serviceRepository;
        }
        public async Task<QueryResultResource<List<ClientViewModel>>> GetDataFromCSVFile(IFormFile Inputfile)
        {
            var result = new QueryResultResource<List<ClientViewModel>>();
            var clients = new List<ClientViewModel>();
            #region Variable Declaration
            DataSet dsexcelRecords = new DataSet();
            IExcelDataReader reader = null;
            Stream FileStream = null;
            #endregion
            FileStream = Inputfile.OpenReadStream();
            if (Inputfile != null && FileStream != null)
            {
                try
                {
                    if (Inputfile.FileName.EndsWith(".xls"))
                        reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                    else if (Inputfile.FileName.EndsWith(".xlsx"))
                        reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                    else
                    {
                        result.Errors.Add("The file format is not supported.");

                    }
                    dsexcelRecords = reader.AsDataSet();
                    reader.Close();
                    if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                    {
                        DataTable dtClientRecords = dsexcelRecords.Tables[0];
                        for (int i = 1; i < dtClientRecords.Rows.Count; i++)
                        {
                            ClientViewModel objclient = new ClientViewModel();
                            var ID = Guid.NewGuid();
                            objclient.Id = ID;
                            objclient.Name_of_business = Convert.ToString(dtClientRecords.Rows[i][0]);
                            objclient.Number = Convert.ToString(dtClientRecords.Rows[i][1]);
                            objclient.Phoneone = Convert.ToString(dtClientRecords.Rows[i][2]);
                            objclient.Phonetwo = Convert.ToString(dtClientRecords.Rows[i][3]);
                            objclient.Nameofcontact = Convert.ToString(dtClientRecords.Rows[i][4]);
                            objclient.Position = Convert.ToString(dtClientRecords.Rows[i][5]);
                            objclient.Completeaddress = Convert.ToString(dtClientRecords.Rows[i][6]);
                            var f = Convert.ToString(dtClientRecords.Rows[i][7]);
                            var Area = await _AreaRepository.GetByName(Convert.ToString(dtClientRecords.Rows[i][7]));
                            if (Area == null)
                            {
                                result.Errors.Add("Area not found");

                            }
                            if (Area != null)
                                objclient.AriaId = _AreaRepository.GetByName(Convert.ToString(dtClientRecords.Rows[i][7])).Result.Id;
                            objclient.Field = Convert.ToString(dtClientRecords.Rows[i][8]);
                            var RELIGION = Convert.ToString(dtClientRecords.Rows[i][9]);
                            Religion myStatus;
                            Enum.TryParse(RELIGION, out myStatus);
                            objclient.Religion = myStatus;
                            objclient.Birthday = Convert.ToString(dtClientRecords.Rows[i][10]);
                            objclient.Facebooklink = Convert.ToString(dtClientRecords.Rows[i][11]);
                            objclient.Instagramlink = Convert.ToString(dtClientRecords.Rows[i][12]);
                            objclient.Website = Convert.ToString(dtClientRecords.Rows[i][13]);
                            objclient.ServiceProvded = Convert.ToString(dtClientRecords.Rows[i][14]);
                            var Serviceproviders = Convert.ToString(dtClientRecords.Rows[i][14]);
                            objclient.Lastfeedback = Convert.ToString(dtClientRecords.Rows[i][15]);
                            var List = Serviceproviders.Split(',').ToList();
                            var Services = new List<Service>();
                            List.ForEach(x =>
                            {
                                Services.Add(new Service()
                                {
                                    Name = x
                                });
                            });
                            var servicesRep = await _serviceRepository.IdList(Services);
                            if (servicesRep.Errors.Count != 0)
                            {
                                result.Errors.AddRange(servicesRep.Errors);
                                result.success = false;
                            }
                            else
                            {
                                List<ServiceProvider> serviceProviders = new List<ServiceProvider>();
                                servicesRep.Data.ToList().ForEach(x =>
                                {
                                    serviceProviders.Add(new ServiceProvider()
                                    {
                                        ClientID = ID,
                                        ServiceId = x.Id
                                    });
                                });
                                await _serviceProviderRepository.AddBulk(serviceProviders);
                            }

                            objclient.Case = Convert.ToString(dtClientRecords.Rows[i][16]);
                            var AccountManager = Convert.ToString(dtClientRecords.Rows[i][17]);
                            var Email = Convert.ToString(dtClientRecords.Rows[i][18]);
                            objclient.Email = Email;


                            var call = Convert.ToString(dtClientRecords.Rows[i][19]).ToLower();
                            if (!string.IsNullOrWhiteSpace(call))
                            {
                                Call callEnumc = (Call)Enum.Parse(typeof(Call), String.Concat(call.Where(c => !char.IsWhiteSpace(c))));
                                objclient.Call = callEnumc;
                            }


                            var type = Convert.ToString(dtClientRecords.Rows[i][20]).ToLower();
                            if (!string.IsNullOrWhiteSpace(type))
                            {
                                Typeclient TypclientEnumc = (Typeclient)Enum.Parse(typeof(Typeclient), String.Concat(type.Where(c => !char.IsWhiteSpace(c))));
                                objclient.Typeclient = TypclientEnumc;
                            }
                            #region string Validation
                            if (string.IsNullOrWhiteSpace(objclient.Phoneone)) { objclient.Phoneone = null; }
                            if (string.IsNullOrWhiteSpace(objclient.Phonetwo)) { objclient.Phonetwo = null; }
                            if (string.IsNullOrWhiteSpace(objclient.Number)) { objclient.Number = null; }

                            if (string.IsNullOrWhiteSpace(objclient.Name_of_business)) { objclient.Name_of_business = null; }
                            if (string.IsNullOrWhiteSpace(objclient.Nameofcontact)) { objclient.Nameofcontact = null; }

                            #endregion



                            var numberExistence = await _clientRepository.IfkeyExistence(objclient.Number, false);
                            if (numberExistence)
                            {
                                result.Errors.Add(objclient.Number + " Phone number is already existence");
                            }

                            var PhoneoneExistence = await _clientRepository.IfkeyExistence(objclient.Phoneone, false);
                            var PhonetwoExistence = await _clientRepository.IfkeyExistence(objclient.Phonetwo, false);
                            if (PhoneoneExistence)
                                result.Errors.Add(objclient.Phoneone + " Phone one number is already existence");

                            if (PhonetwoExistence)
                                result.Errors.Add(objclient.Phonetwo+ " Phone two number is already existence");

                            if (string.IsNullOrEmpty(objclient.Number))
                            {
                                result.Errors.Add(objclient.Number + " Phone number is Required");
                            }
                            var salesDirector = await _customerRepository.Checke(x => x.Email == AccountManager);
                            if (salesDirector.Data != null)
                                objclient.AccountManager = salesDirector.Data.Id;

                            var teamMember = await _teamMemberRepository.Checke(x => x.Email == AccountManager);
                            if (teamMember.Data != null)
                                objclient.AccountManager = teamMember.Data.Id;

                            var teamLeader = await _teamLeaderRepository.Checke(x => x.Email == AccountManager);
                            if (teamLeader.Data != null)
                                objclient.AccountManager = teamLeader.Data.Id;

                            var userManager = await _userManager.FindByEmailAsync(AccountManager);
                            if (userManager != null)
                                objclient.AccountManager = userManager.ParentUserId;
                            else
                            {
                                result.Errors.Add(Convert.ToString(dtClientRecords.Rows[i][17]) + " Account manager not found");
                            }
                            
                            clients.Add(objclient);
                        }
                        if (result.Errors.Count != 0)
                        {
                            result.success = false;
                            return result;
                        }


                    }

                    var errors = result.Errors;
                    var ClientViews = _map.Map<List<Client>>(clients);
                    ClientViews.ForEach(x =>
                    {
                        x.ModifiedBy = user.GetUserId();
                        x.ModifiedOn = DateTime.Now;
                        x.Status = UserStatus.Active;
                    });

                    _clientRepository.AddBulk(ClientViews);

                    if (result.Errors.Count != 0)
                        result.success = false;
                    return result;
                }
                catch (Exception ex)
                {
                    result.Errors.Add(ex.Message);
                    result.success = false;
                    return result;
                }
            }
            result.success = true;
            result.Total = clients.Count;
            return result;
        }
    }
}

