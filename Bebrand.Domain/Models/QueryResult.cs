using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class QueryResult<T>
    {
        public IEnumerable<T> data { get; set; }
        public bool success { get; set; } = true;
        public List<String> Errors { get; set; } = new List<string>();
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public bool IsFirstPage { get; set; } = false;
        public bool IsLastPage { get; set; } = false;
        public int Total { get; set; }
    }
    public class QueryMultipleResult<T>
    {
        #region [Properties]
        public bool IsSucceeded { get; set; } = true;
        public List<String> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
        public int Total { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public bool IsFirstPage { get; set; } = false;
        public bool IsLastPage { get; set; } = false;
        public int PageCount { get; set; } = 0;
        #endregion

        #region [ctor]
        public QueryMultipleResult() { }
        public QueryMultipleResult(T data)
        {
            this.Data = data;
            
        }
        public QueryMultipleResult(String error)
        {
            this.Errors.Add(error);
            this.IsSucceeded = false;
        }
        #endregion
    }
}
