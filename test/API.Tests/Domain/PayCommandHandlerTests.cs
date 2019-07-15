using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Commands;
using API.Domain.Events;
using API.Services;
using API.Services.FakeAcquiringBankImpls;
using CreditCardValidator;
using Xunit;

namespace API.Tests.Domain
{
    public class PayCommandHandlerTests
    {
        [Fact]
        public async Task GivenPayCommandWhenCommandIsExecutedThenEmitsPaymentSucceededIsEmitted()
        {
            // arrange
            var commandHandler = new PayCommandHandler(new FakeAcquiringBankServiceWithSuccessfulResponse());
            var aggregateId = PaymentId.New;
            var command = new PayCommand(aggregateId, CreateRequest());
            var aggregate = new PaymentAggregate(aggregateId);

            // act
            await commandHandler.ExecuteAsync(aggregate, command, CancellationToken.None);

            // assert
            Assert.NotNull(aggregate.UncommittedEvents
                .Single(x => x.AggregateEvent.GetType() == typeof(PaymentSucceeded)));
        }
        
        [Fact]
        public async Task GivenPayCommandWhenCommandIsExecutedThenPaymentFailedIsEmitted()
        {
            // arrange
            var commandHandler = new PayCommandHandler(new FakeAcquiringBankServiceWithFailedResponse());
            var aggregateId = PaymentId.New;
            var command = new PayCommand(aggregateId, CreateRequest());
            var aggregate = new PaymentAggregate(aggregateId);

            // act
            await commandHandler.ExecuteAsync(aggregate, command, CancellationToken.None);

            // assert
            Assert.NotNull(aggregate.UncommittedEvents
                .Single(x => x.AggregateEvent.GetType() == typeof(PaymentFailed)));
        }

        private AcquiringBankPaymentRequest CreateRequest() => new AcquiringBankPaymentRequest(
            CreditCardFactory.RandomCardNumber(CardIssuer.Visa),
            8,
            2019,
            "Alfred Tarski",
            2000,
            "USD",
            966
        );
    }
}