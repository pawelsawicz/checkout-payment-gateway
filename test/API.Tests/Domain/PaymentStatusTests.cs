using API.Domain;
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

            Assert.Equal("************7349", paymentStatus.Mask().CardNumber);
        }
        
        [Fact]
        public void GivenMaskedCardNumberWhenMaskThenItIsIdempotent()
        {
            var paymentStatus = new PaymentStatus
            {
                CardNumber = "4698050763557349"
            }.Mask();

            Assert.Equal("************7349", paymentStatus.Mask().CardNumber);
        }
    }
}