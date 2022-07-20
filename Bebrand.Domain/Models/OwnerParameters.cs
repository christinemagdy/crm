using Bebrand.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class OwnerParameters
    {

        const int maxPageSize = 50;
        public int PageNumber { get; set; }
        private int _pageSize = 50;
        public string Sort { get; set; }
        public string Order { get; set; }
        public Guid AcccountManager { get; set; }
        public Guid AriaId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string key { get; set; }
        public int PageSize { get; set; }

    }
}
