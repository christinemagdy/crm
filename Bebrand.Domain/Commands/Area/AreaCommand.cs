using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Domain.Commands.Area
{
    public class AreaCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
