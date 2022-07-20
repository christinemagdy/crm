using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Events.Client;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bebrand.Domain.EventHandlers
{
    public class ClientEventHandler :
     INotificationHandler<ClientRegisterdEvent>,
     INotificationHandler<ClientUpdatedEvent>,
     INotificationHandler<ClientRemovedEvent>
    {
        public Task Handle(ClientUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(ClientRegisterdEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(ClientRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
