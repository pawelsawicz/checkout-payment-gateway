using EventFlow.Commands;
using EventFlow.Core;

namespace API.Domain
{
    public class PayCommand : Command<PaymentAggregate, PaymentId>
    {
        public PayCommand(PaymentId aggregateId) : base(aggregateId)
        {
        }

        public PayCommand(PaymentId aggregateId, ISourceId sourceId) : base(aggregateId, sourceId)
        {
        }
    }
}