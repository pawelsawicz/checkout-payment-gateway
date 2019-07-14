using System.Linq;
using API.Domain;
using API.Services;
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
            var bankPaymentResponse = new AcquiringBankPaymentResponse();
            
            // act
            paymentAggregate.SetPaymentSuccessful(bankPaymentResponse);

            // assert
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentSucceeded)));
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentFailedThenEventIsEmitted()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var bankPaymentResponse = new AcquiringBankPaymentResponse();

            // act
            paymentAggregate.SetPaymentFailed(bankPaymentResponse);
            
            // assert
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentFailed)));
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentFailedTwiceThenDomainExceptionIsThrown()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var bankPaymentResponse = new AcquiringBankPaymentResponse();
            paymentAggregate.SetPaymentFailed(bankPaymentResponse);

            // act
            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.SetPaymentFailed(bankPaymentResponse));
            
            // assert
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentSetPaymentSuccessfulTwiceThenDomainExceptionIsThrown()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var bankPaymentResponse = new AcquiringBankPaymentResponse();
            paymentAggregate.SetPaymentSuccessful(bankPaymentResponse);

            // act
            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.SetPaymentSuccessful(bankPaymentResponse));
            
            // assert
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
    }
}