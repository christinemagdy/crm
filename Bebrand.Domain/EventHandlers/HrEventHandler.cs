using Bebrand.Domain.Events.Hr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bebrand.Domain.EventHandlers
{
    public class HrEventHandler :
      INotificationHandler<HrRegisteredEvent>,
      INotificationHandler<HrUpdatedEvent>,
      INotificationHandler<HrRemovedEvent>
    {
        public Task Handle(HrUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(HrRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(HrRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
