using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.ServiceProvider
{
    public class UpdateServiceProviderViewModel
    {
        public Guid Id { get; set; }
        public Guid ClientID { get; set; }
        public Guid ServiceId { get; set; }
    }
}
