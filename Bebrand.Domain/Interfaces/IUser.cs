using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        string GetUserId();
        bool IsAuthenticated();
        string GetParentUserId();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
