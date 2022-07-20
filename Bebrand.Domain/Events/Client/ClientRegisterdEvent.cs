using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Events.Client
{
    public class ClientRegisterdEvent : Event
    {
        public ClientRegisterdEvent(Guid id, string name_of_business, string Email,
            string number, string nameofcontact, string position, string Completeaddress, Guid articalid,
            string Field, Religion religion, string Facebooklink, string Instagramlink, string Website,
            string Lastfeedback, string ServiceProvded, string Case, Guid AccountManager)
        {
            AggregateId = id;
            Id = id;
            Name_of_business = name_of_business;
            this.Email = Email;
            Number = number;
            Nameofcontact = nameofcontact;
            Position = position;
            this.Completeaddress = Completeaddress;
            this.AriaId = articalid;
            this.Field = Field;
            this.Religion = religion;
            this.Facebooklink = Facebooklink;
            this.Instagramlink = Instagramlink;
            this.Website = Website;
            this.Lastfeedback = Lastfeedback;
            this.ServiceProvded = ServiceProvded;
            this.Case = Case;
            this.AccountManager = AccountManager;

        }

        public Guid Id { get; set; }
        public string Name_of_business { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Nameofcontact { get; set; }
        public string Position { get; set; }
        public string Completeaddress { get; set; }

        public Guid AriaId { get; set; }

        public string Field { get; set; }
        public Religion Religion { get; set; }
        public DateTime Birthday { get; set; }
        public string Facebooklink { get; set; }
        public string Instagramlink { get; set; }
        public string Website { get; set; }
        public string Lastfeedback { get; set; }
        public string ServiceProvded { get; set; }
        public string Case { get; set; }
        public UserStatus Status { get; set; }
        public Guid AccountManager { get; set; }

    }
}
