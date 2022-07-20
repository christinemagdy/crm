using Bebrand.Domain.Events.TeamLeader;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bebrand.Domain.EventHandlers
{
    public class TeamLeaderEventHandler :
     INotificationHandler<TeamLeaderRegisteredEvent>,
     INotificationHandler<TeamLeaderUpdatedEvent>,
     INotificationHandler<TeamLeaderRemovedEvent>
    {

        public Task Handle(TeamLeaderUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TeamLeaderRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TeamLeaderRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
