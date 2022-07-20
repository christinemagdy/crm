using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.VacanciesMail
{
    public class VacanciesMailMemberCommand : Command
    {
        public Guid Id { get; set; }
        public string UniqueIds { get; set; }
        public string Subject { get; set; }
        public string Sender { get; set; }

        public string TextBody { get; set; }
        public string Attachement { get; set; }
        public Guid JobId { get; set; }

    }
}
