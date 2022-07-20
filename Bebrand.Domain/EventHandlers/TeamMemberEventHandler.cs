using Bebrand.Domain.Events.TeamMember;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bebrand.Domain.EventHandlers
{
    public class TeamMemberEventHandler :
    INotificationHandler<TeamMemberRegisteredEvent>,
    INotificationHandler<TeamMemberUpdatedEvent>,
    INotificationHandler<TeamMemberRemovedEvent>
    {

        public Task Handle(TeamMemberUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TeamMemberRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TeamMemberRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
