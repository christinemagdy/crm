using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels
{
    public class QueryResultResource<T>
    {

        public IEnumerable<T> data { get; set; }
        public bool success { get; set; } = true;
        public List<String> Errors { get; set; } = new List<string>();
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int Total { get; set; }
    }
}
