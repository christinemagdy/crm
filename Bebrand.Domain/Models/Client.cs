using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Models
{
    public class Client : EntityInfo, IAggregateRoot
    {
        public Client(Guid id, string name_of_business, string Email,
            string number, string nameofcontact, string position, string Completeaddress, Guid articalid,
            string Field, Religion religion, string Facebooklink, string Instagramlink, string Website,
            string Lastfeedback, string ServiceProvded, string Case, Guid AccountManager,
            string ModifiedBy, DateTime modifiedOn, DateTime date, UserStatus status, string birthday, Call call, Typeclient typeclient, string phoneone, string phonetwo)
        {
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
            Status = status;
            this.ModifiedBy = ModifiedBy;
            this.ModifiedOn = modifiedOn;
            Date = date;
            Birthday = birthday;
            Call = call;
            Typeclient = typeclient;
            Phoneone = phoneone;
            Phonetwo = phonetwo;
        }


        public Client(Guid id, string name_of_business, string Email,
           string number, string nameofcontact, string position, string Completeaddress, Guid articalid,
           string Field, Religion religion, string Facebooklink, string Instagramlink, string Website,
           string Lastfeedback, string ServiceProvded, string Case, Guid AccountManager,
           string ModifiedBy, DateTime modifiedOn, UserStatus status, string birthday, Call call, Typeclient typeclient, string phoneone, string phonetwo)
        {
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
            Status = status;
            this.ModifiedBy = ModifiedBy;
            this.ModifiedOn = modifiedOn;
            Birthday = birthday;
            Call = call;
            Typeclient = typeclient;
            Phoneone = phoneone;
            Phonetwo = phonetwo;
        }

        // Empty constructor for EF
        protected Client() { }

        public string Name_of_business { get; set; }

        public string Email { get; set; }
        public string Number { get; set; }
        public string Phoneone { get; set; }
        public string Phonetwo { get; set; }
        public string Nameofcontact { get; set; }
        public string Position { get; set; }
        public string Completeaddress { get; set; }

        public Guid AriaId { get; set; }
        public Area Area { get; set; }
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
        public ICollection<ServiceProvider> ServiceProviders { get; set; }
        public string Case { get; set; }
        public Guid AccountManager { get; set; }


    }
}
