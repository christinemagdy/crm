﻿using Bebrand.Application.ViewModels.Services;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.ClientView
{
    public class CreateClientViewModel
    {
        public string Name_of_business { get; set; }
        public string Number { get; set; }            
        public string Phoneone { get; set; }
        public string Phonetwo { get; set; }
        public string Nameofcontact { get; set; }
        public string Position { get; set; }
        public string Completeaddress { get; set; }
        public string Email { get; set; }
        public Guid AriaId { get; set; }
        public string Field { get; set; }
        public Religion Religion { get; set; }
        public Call Call { get; set; }
        public Typeclient Typeclient { get; set; }
        public string Birthday { get; set; }
        public string Facebooklink { get; set; }
        public string Instagramlink { get; set; }
        public string Website { get; set; }
        public string Lastfeedback { get; set; }
        public string ServiceProvded { get; set; }
        public string Case { get; set; }
        public Guid AccountManager { get; set; }
        public List<Service> ServiceProviders { get; set; } = new List<Service>();
    }
}
