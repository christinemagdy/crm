using Bebrand.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Client
{
    public class UpdateClientCommand : ClientCommand
    {
        public UpdateClientCommand(Guid id, string name_of_business, string Email,
            string number, string nameofcontact, string position, string Completeaddress, Guid articalid,
            string Field, Religion religion, string Facebooklink, string Instagramlink, string Website,
            string Lastfeedback, string ServiceProvded, string Case, Guid AccountManager, Call call, Typeclient typeclient, string phoneone, string phonetwo)
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
            Call = call;
            Typeclient = typeclient;
            Phoneone = phoneone;
            Phonetwo = phonetwo;

        }
    }
}
