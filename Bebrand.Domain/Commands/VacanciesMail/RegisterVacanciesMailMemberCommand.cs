using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.VacanciesMail
{
    public class RegisterVacanciesMailMemberCommand : VacanciesMailMemberCommand
    {
        public RegisterVacanciesMailMemberCommand(string uniqueIds, string subject,
            string textBody, string attachement, Guid jobId,string sender)
        {
            UniqueIds = uniqueIds;
            Subject = subject;
            TextBody = textBody;
            Attachement = attachement;
            JobId = jobId;
        }
        public override bool IsValid()
        {
            return true;
        }
    }
}
