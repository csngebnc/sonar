using MassTransit;
using MediatR;
using Order.Interface.Events.LockerAggregate;
using Shipping.Domain.Entities.LockerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.LockerAggregate
{
    public class DeleteLockerCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteLockerCommandHandler : IRequestHandler<DeleteLockerCommand, Unit>
    {
        private readonly ILockerRepository lockerRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public DeleteLockerCommandHandler(ILockerRepository lockerRepository, IPublishEndpoint publishEndpoint)
        {
            this.lockerRepository = lockerRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteLockerCommand request, CancellationToken cancellationToken)
        {
            var locker = await lockerRepository.SingleAsync(x => x.Id == request.Id);

            locker.IsDeleted = true;

            await lockerRepository.UpdateAsync(locker);
            await publishEndpoint.Publish(new LockerDeletedEvent
            {
                LockerId = request.Id,
            });

            return Unit.Value;
        }
    }
}
