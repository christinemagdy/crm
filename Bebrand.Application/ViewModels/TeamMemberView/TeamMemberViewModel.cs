using Bebrand.Application.ViewModels.TeamLeaderView;
using System;

namespace Bebrand.Application.ViewModels.TeamMemberView
{
    public class TeamMemberViewModel
    {
        public Guid Id { get; set; }
        public string FName { get; private set; }

        public string LName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

      
        public TeamLeaderViewModel TeamLeader { get; set; }
    }
}
