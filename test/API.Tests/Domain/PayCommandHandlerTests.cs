using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Services;
using Xunit;

namespace API.Tests.Domain
{
    public class PayCommandHandlerTests
    {
        [Fact]
        public async Task GivenPayCommandHandlerWhenPaymentIsApprovedThenChangeAggregateToSuccessful()
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
        public async Task GivenPayCommandHandlerWhenPaymentIsFailedThenChangeAggregateToFailed()
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

        private BankPaymentRequest CreateRequest() => new BankPaymentRequest
        {
            CardNumber = Guid.NewGuid().ToString(),
            ExpiryMonth = 8,
            ExpiryDate = 2019,
            Amount = 2000,
            Name = "Alfred Tarski",
            CurrencyCode = "USD",
            Cvv = 966
        };
    }
}