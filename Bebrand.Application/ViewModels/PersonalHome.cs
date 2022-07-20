using Bebrand.Application.ViewModels.ClientView;
using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Application.ViewModels.TeamLeaderView;
using Bebrand.Application.ViewModels.TeamMemberView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels
{
    public class PersonalHome
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime BirthDate { get; set; }

        public List<ClientViewModel> ClientViews { get; set; } = new List<ClientViewModel>();
        public List<TeamLeaderViewModel> teamLeaderViewModels { get; set; } = new List<TeamLeaderViewModel>();
        public List<TeamMemberViewModel> teamMemberViewModels { get; set; } = new List<TeamMemberViewModel>();
        public List<CustomerViewModel> SalesDirector { get; set; } = new List<CustomerViewModel>();


    }
}
