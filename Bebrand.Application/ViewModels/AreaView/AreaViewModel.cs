using Bebrand.Application.ViewModels.ClientView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.AreaView
{
    public class AreaViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<ClientViewModel> Clients { get; set; }
    }
}
