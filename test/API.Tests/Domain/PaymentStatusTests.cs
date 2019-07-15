using System;
using API.Domain;
using Shouldly;
using Xunit;

namespace API.Tests.Domain
{
    public class PaymentStatusTests
    {
        [Fact]
        public void GivenCardNumberWhenMaskThenCardNumberIsMasked()
        {
            var cardNumber = "4698050763557349";
            var paymentStatus = CreatePaymentStatus(cardNumber);
            

            paymentStatus.Mask().CardNumber.ShouldBe("************7349");
        }

        [Fact]
        public void MaskMethodIsIdempotent()
        {
            var cardNumber = "4698050763557349";
            var paymentStatus = CreatePaymentStatus(cardNumber).Mask();

            paymentStatus.Mask().CardNumber.ShouldBe("************7349");
        }

        private PaymentStatus CreatePaymentStatus(string cardNumber) => new PaymentStatus(
            Guid.NewGuid().ToString(),
            "Approved",
            cardNumber,
            6,
            2019,
            "Alfred Tarski",
            2000,
            "USD");
    }
}