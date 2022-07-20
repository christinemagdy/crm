using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.Interfaces
{
    public interface ImailAppService : IDisposable
    {
        IEnumerable<string> GetUnreadMails();
        void GetAllMails();
    }
}
