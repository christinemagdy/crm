using AutoMapper;
using Bebrand.Application.Interfaces;
using Bebrand.Application.ViewModels.VacanciesMail;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bebrand.Application.Services
{
    public class MailAppService : ImailAppService
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #region [vars]
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IJobsRepository _jobsRepository;
        private readonly IVacanciesMailAppService _vacanciesMailAppService;
        private readonly Appsettings _config;

        private readonly bool ssl = true;

        public MailAppService(IWebHostEnvironment hostingEnvironment, IOptions<Appsettings> config, IJobsRepository jobsRepository, IVacanciesMailAppService vacanciesMailAppService)
        {
            _hostingEnvironment = hostingEnvironment;
            _jobsRepository = jobsRepository;
            _config = config.Value;

            _vacanciesMailAppService = vacanciesMailAppService;
        }
        #endregion


        public IEnumerable<string> GetUnreadMails()
        {
            var messages = new List<string>();
            _vacanciesMailAppService.UniqueIds();
            using (var client = new ImapClient())
            {
                client.Connect(_config.mailServer, _config.port, ssl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_config.login, _config.password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                var results = inbox.Search(SearchOptions.All, SearchQuery.Not(SearchQuery.Seen));
                foreach (var uniqueId in results.UniqueIds)
                {
                    var message = inbox.GetMessage(uniqueId);

                    messages.Add(message.Subject);

                    //Mark message as read
                    //inbox.AddFlags(uniqueId, MessageFlags.Seen, true);
                }

                client.Disconnect(true);
            }

            return messages;
        }
        public void GetAllMails()
        {
            var uniqueIdsList = _vacanciesMailAppService.UniqueIds();
            var VacanciesList = new List<CreateVacanciesMailViewModel>();
            using (var client = new ImapClient())
            {
                client.Connect(_config.mailServer, _config.port, ssl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(_config.login, _config.password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                //Get mails by Title
                var jobs = _jobsRepository.Get().ConfigureAwait(true).GetAwaiter().GetResult().Data.ToList();
                foreach (var job in jobs)
                {
                    var results = inbox.Search(SearchOptions.All, SearchQuery.SubjectContains(job.JobsTitle));
                    foreach (var uniqueId in results.UniqueIds.Where(x => !uniqueIdsList.Contains(x.Id.ToString())))
                    {
                        var message = inbox.GetMessage(uniqueId);
                        var attachements = message.Attachments.ToList();
                        //_fileAppService.Save(attachements);
                        foreach (var attachment in attachements)
                        {
                            var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                            FileInfo fi = new FileInfo(fileName);
                            var ex = fi.Extension.ToLower();
                            var valid = _config.Extensions.Contains(ex);
                            if (valid)
                            {
                                string webRootPath = _hostingEnvironment.ContentRootPath;
                                var fullPath = Path.Combine(webRootPath, "Bebrand_Uploads");
                                var file = Path.Combine(fullPath, fileName);
                                using (var stream = File.Create(file))
                                {
                                    if (attachment is MessagePart)
                                    {
                                        var rfc822 = (MessagePart)attachment;
                                        rfc822.Message.WriteTo(stream);
                                    }
                                    else
                                    {
                                        var part = (MimePart)attachment;
                                        part.Content.DecodeTo(stream);
                                    }
                                }

                                VacanciesList.Add(new CreateVacanciesMailViewModel()
                                {
                                    Attachement = fileName,
                                    JobId = job.Id,
                                    Subject = message.Subject,
                                    TextBody = message.TextBody,
                                    UniqueIds = uniqueId.Id.ToString(),
                                    Sender = message.From.Mailboxes.Select(x => x.Address).FirstOrDefault()
                                });
                            }
                        }
                    }
                }
                if (VacanciesList.Count != 0)
                    _vacanciesMailAppService.Register(VacanciesList);

                client.Disconnect(true);
            }


        }
    }
}

