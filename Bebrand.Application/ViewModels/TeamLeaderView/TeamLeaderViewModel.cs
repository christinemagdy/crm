﻿using Bebrand.Application.ViewModels.CustomerView;
using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.TeamLeaderView
{
    public class TeamLeaderViewModel
    {
        public Guid Id { get; set; }
        public string FName { get; private set; }

        public string LName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public Guid SalesDirectorId { get; set; }
        public CustomerViewModel SalesDirector { get; private set; }
    }
}
