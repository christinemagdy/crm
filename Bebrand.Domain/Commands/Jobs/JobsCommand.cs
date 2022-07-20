using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Models;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Jobs
{
    public class JobsCommand : Command
    {
        public Guid Id { get; set; }
        public string JobsTitle { get; set; }
        public List<Models.VacanciesMail> vacanciesMails { get; set; }

        public UserStatus Status { get; set; }

    }
}
