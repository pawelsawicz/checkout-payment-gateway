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
        public void GivenInitialStateWhenSuccessPaymentThenPaymentSucceededIsEmitted()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var paymentStatus = new PaymentStatus();
            
            // act
            paymentAggregate.SuccessPayment(paymentStatus);

            // assert
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentSucceeded)));
        }
        
        [Fact]
        public void GivenInitialStateWhenFailPaymentThenPaymentFailedIsEmitted()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var paymentStatus = new PaymentStatus();

            // act
            paymentAggregate.FailPayment(paymentStatus);
            
            // assert
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentFailed)));
        }
        
        [Fact]
        public void GivenInitialStateWhenFailPaymentInvokedTwiceThrowsDomainException()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var paymentStatus = new PaymentStatus();
            paymentAggregate.FailPayment(paymentStatus);

            // act
            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.FailPayment(paymentStatus));
            
            // assert
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
        
        [Fact]
        public void GivenInitialStateWhenSuccessPaymentInvokedTwiceThrowsDomainException()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var paymentStatus = new PaymentStatus();
            paymentAggregate.SuccessPayment(paymentStatus);

            // act
            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.SuccessPayment(paymentStatus));
            
            // assert
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
    }
}