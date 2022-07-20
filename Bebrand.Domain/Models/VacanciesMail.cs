using Bebrand.Domain.Core;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class VacanciesMail : EntityInfo, IAggregateRoot
    {
        public VacanciesMail() { }
        public VacanciesMail(string uniqueIds, string subject, string textBody, string attachement, Guid jobId, string modifiedBy, string sender)
        {
            Id = Guid.NewGuid();
            UniqueIds = uniqueIds;
            Subject = subject;
            TextBody = textBody;
            Attachement = attachement;

            JobId = jobId;
            ModifiedOn = DateTime.Now;
            ModifiedBy = modifiedBy;
            Sender = sender;
        }

        public string UniqueIds { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string Attachement { get; set; }
        public string Sender { get; set; }

        public virtual Jobs Jobs { get; set; }
        public Guid JobId { get; set; }
    }
}
