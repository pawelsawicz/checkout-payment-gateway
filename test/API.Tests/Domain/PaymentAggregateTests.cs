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
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var bankPaymentResponse = new BankPaymentResponse();
            
            paymentAggregate.SetPaymentSuccessful(bankPaymentResponse);

            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentSucceeded)));
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentFailedThenEventIsEmitted()
        {
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var bankPaymentResponse = new BankPaymentResponse();

            paymentAggregate.SetPaymentFailed(bankPaymentResponse);
            Assert.NotNull(paymentAggregate.UncommittedEvents.Single(x =>
                x.AggregateEvent.GetType() == typeof(PaymentFailed)));
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentFailedTwiceThenDomainExceptionIsThrown()
        {
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var bankPaymentResponse = new BankPaymentResponse();
            paymentAggregate.SetPaymentFailed(bankPaymentResponse);

            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.SetPaymentFailed(bankPaymentResponse));
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
        
        [Fact]
        public void GivenBankPaymentResponseWhenSetPaymentSetPaymentSuccessfulTwiceThenDomainExceptionIsThrown()
        {
            var paymentAggregate = new PaymentAggregate(PaymentId.New);
            var bankPaymentResponse = new BankPaymentResponse();
            paymentAggregate.SetPaymentSuccessful(bankPaymentResponse);

            var domainError = Assert.Throws<DomainError>(
                () => paymentAggregate.SetPaymentSuccessful(bankPaymentResponse));
            Assert.Equal("Payment has been already processed", domainError.Message);
        }
    }
}