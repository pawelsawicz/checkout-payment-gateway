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
            var paymentStatus = new PaymentStatus
            {
                CardNumber = "4698050763557349"
            };

            paymentStatus.Mask().CardNumber.ShouldBe("************7349");
        }

        [Fact]
        public void MaskMethodIsIdempotent()
        {
            var paymentStatus = new PaymentStatus
            {
                CardNumber = "4698050763557349"
            }.Mask();

            paymentStatus.Mask().CardNumber.ShouldBe("************7349");
        }
    }
}