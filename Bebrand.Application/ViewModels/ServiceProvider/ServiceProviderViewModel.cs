using Bebrand.Application.ViewModels.Services;
using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.ServiceProvider
{
    public class ServiceProviderViewModel
    {

        public ServiceViewModel Service { protected get; set; }
        public Guid Id { get { return Service.Id; } set { } }
        public string Name
        {
            get { return Service.Name; }
            set { }
        }
    }
}
