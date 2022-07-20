using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class Jobs : EntityInfo, IAggregateRoot
    {
        public Jobs(Guid id, string jobsTitle)
        {
            Id = id;
            JobsTitle = jobsTitle;
        }
        public string JobsTitle { get; set; }  
        public ICollection<VacanciesMail> vacanciesMails { get; set; }
    }
}
