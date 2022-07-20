using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class Appsettings
    {
        public string token { get; set; }
        public string domain { get; set; }
        public int port { get; set; }
        public IList<string> Extensions { get; set; } = new List<string>();
        public string mailServer { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
