using System.Linq;
using API.Domain;
using API.Domain.Events;
using EventFlow.Exceptions;
using Xunit;

namespace API.Tests.Domain
{
    public class PaymentAggregateTests
    {

        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentSuccessfulThenEventIsEmitted()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var paymentStatus = new PaymentStatus();
            
            // act
            paymentAggregate.SetPaymentSuccessful(paymentStatus);

            // assert
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentSucceeded)));
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentFailedThenEventIsEmitted()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var paymentStatus = new PaymentStatus();

            // act
            paymentAggregate.SetPaymentFailed(paymentStatus);
            
            // assert
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentFailed)));
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentFailedTwiceThenDomainExceptionIsThrown()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var paymentStatus = new PaymentStatus();
            paymentAggregate.SetPaymentFailed(paymentStatus);

            // act
            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.SetPaymentFailed(paymentStatus));
            
            // assert
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentSetPaymentSuccessfulTwiceThenDomainExceptionIsThrown()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var paymentStatus = new PaymentStatus();
            paymentAggregate.SetPaymentSuccessful(paymentStatus);

            // act
            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.SetPaymentSuccessful(paymentStatus));
            
            // assert
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
    }
}