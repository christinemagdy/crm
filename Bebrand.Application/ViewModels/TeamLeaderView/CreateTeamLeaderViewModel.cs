using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.TeamLeaderView
{
    public class CreateTeamLeaderViewModel
    {

        public string FName { get;  set; }

        public string LName { get;  set; }

        public string Email { get;  set; }

        public string Password { get; set; }
        public DateTime BirthDate { get;  set; }

        public Guid SalesDirectorId { get; set; }
    }
}
