using Bebrand.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetDevPack.Messaging;

namespace Bebrand.Domain.Commands.Hr
{
    public abstract class HrCommand : Command
    {
        public Guid Id { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public DateTime BirthDate { get; set; }

        public Status Status { get; set; }
    }
}