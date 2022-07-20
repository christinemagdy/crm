using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.VacanciesMail
{
    public class CreateVacanciesMailViewModel
    {
        public string UniqueIds { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string Attachement { get; set; }
        public string Sender { get; set; }
        public Guid JobId { get; set; }
        //public List<CreateVacanciesMailViewModel> vacanciesMailViewModels { get; set; }
    }
}
