using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Enums
{
    public enum Roles
    {
        SuperAdmin,
        Salesdirector,
        Teamleader,
        Teammember,
        Hr
    }

    public enum Status
    {
        Active = 1,
        Deactivate = 0,
        Updated = 2
    }
    public enum Religion
    {
        Muslim = 1,
        Christian = 2
    }

    public enum Call
    {
        called = 1,
        newcase = 2,
        followup = 3,
        answered = 4
    }
    public enum Typeclient
    {
        oldclient = 2,
        newclient = 1
    }

}
