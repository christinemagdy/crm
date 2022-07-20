using System;
using Bebrand.Domain.Enums;
//using Bebrand.Domain.Core.Commands;
//using Bebrand.Domain.Enum;
using NetDevPack.Messaging;

namespace Bebrand.Domain.Commands
{
    public abstract class CustomerCommand : Command
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