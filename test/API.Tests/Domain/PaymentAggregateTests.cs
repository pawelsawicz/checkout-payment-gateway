using System;
using System.Linq;
using API.Domain;
using API.Domain.Events;
using CreditCardValidator;
using EventFlow.Exceptions;
using Xunit;

namespace API.Tests.Domain
{
    public class PaymentAggregateTests
    {
        private readonly PaymentStatus _paymentStatus;

        public PaymentAggregateTests()
        {
            _paymentStatus = new PaymentStatus(
                Guid.NewGuid().ToString(),
                "Approved",
                CreditCardFactory.RandomCardNumber(CardIssuer.Visa),
                6,
                2019,
                "Alfred Tarski",
                2000,
                "USD");
        }

        [Fact]
        public void GivenInitialStateWhenSuccessPaymentThenPaymentSucceededIsEmitted()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            
            // act
            paymentAggregate.SuccessPayment(_paymentStatus);

            // assert
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentSucceeded)));
        }
        
        [Fact]
        public void GivenInitialStateWhenFailPaymentThenPaymentFailedIsEmitted()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);

            // act
            paymentAggregate.FailPayment(_paymentStatus);
            
            // assert
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentFailed)));
        }
        
        [Fact]
        public void GivenInitialStateWhenFailPaymentInvokedTwiceThrowsDomainException()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            paymentAggregate.FailPayment(_paymentStatus);

            // act
            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.FailPayment(_paymentStatus));
            
            // assert
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
        
        [Fact]
        public void GivenInitialStateWhenSuccessPaymentInvokedTwiceThrowsDomainException()
        {
            // arrange
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            paymentAggregate.SuccessPayment(_paymentStatus);

            // act
            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.SuccessPayment(_paymentStatus));
            
            // assert
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
    }
}