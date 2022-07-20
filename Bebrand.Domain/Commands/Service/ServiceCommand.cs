using NetDevPack.Messaging;
using System;

namespace Bebrand.Domain.Commands.Service
{
    public class ServiceCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
