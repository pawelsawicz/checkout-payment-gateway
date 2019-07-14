using API.Models;
using CreditCardValidator;
using Shouldly;
using Xunit;

namespace API.Tests.Controllers
{
    public class PaymentRequestValidatorTests
    {
        private readonly PaymentRequestValidator sut;

        public PaymentRequestValidatorTests()
        {
            sut = new PaymentRequestValidator();
        }
        
        [Fact]
        public void WhenCorrectDataThenValidationPasses()
        {
            var request = new PaymentRequestBuilder().Build();
            
            var result = sut.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        public void CardNumberMustBeNotEmpty(string cardNumber)
        {
            var request = new PaymentRequestBuilder()
                .WithCardNumber(cardNumber)
                .Build();
            
            var result = sut.Validate(request);

            result.IsValid.ShouldBeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(13)]
        public void ExpiryMonthMustBeBetween1and12(int expiryMonth)
        {
            var request = new PaymentRequestBuilder()
                .WithExpiryMonth(expiryMonth)
                .Build();
            
            var result = sut.Validate(request);

            result.IsValid.ShouldBeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(13)]
        public void ExpiryDateMustBeCurrentYearOrFuture(int expiryDate)
        {
            var request = new PaymentRequestBuilder()
                .WithExpiryDate(expiryDate)
                .Build();
            
            var result = sut.Validate(request);

            result.IsValid.ShouldBeFalse();   
        }
        
        [Theory]
        [InlineData("")]
        public void NameMustBeNotEmpty(string name)
        {
            var request = new PaymentRequestBuilder()
                .WithName(name)
                .Build();
            
            var result = sut.Validate(request);

            result.IsValid.ShouldBeFalse();
        }
        
        [Theory]
        [InlineData(0)]
        public void AmountMustBeGreaterThanZero(decimal amount)
        {
            var request = new PaymentRequestBuilder()
                .WithAmount(amount)
                .Build();
            
            var result = sut.Validate(request);

            result.IsValid.ShouldBeFalse();
        }

        [Theory]
        [InlineData("")]
        public void CurrencyCodeMustNotBeEmpty(string currencyCode)
        {
            var request = new PaymentRequestBuilder()
                .WithCurrencyCode(currencyCode)
                .Build();
            
            var result = sut.Validate(request);

            result.IsValid.ShouldBeFalse();
        }
        
        [Theory]
        [InlineData(99)]
        [InlineData(10000)]
        public void CvvMustBeBetween100and999(int cvv)
        {
            var request = new PaymentRequestBuilder()
                .WithCvv(cvv)
                .Build();
            
            var result = sut.Validate(request);

            result.IsValid.ShouldBeFalse();
        }

        private class PaymentRequestBuilder
        {
            private string cardNumber { get; set; }

            private int expiryMonth { get; set; }
        
            private int expiryDate { get; set; }

            private string name { get; set; }
        
            private decimal amount { get; set; }

            private string currencyCode { get; set; }

            private int cvv { get; set; }

            public PaymentRequestBuilder()
            {
                cardNumber = CreditCardFactory.RandomCardNumber(CardIssuer.Visa);
                expiryMonth = 1;
                expiryDate = 2019;
                name = "Alfred Tarski";
                amount = 2000;
                currencyCode = "USD";
                cvv = 259;
            }

            public PaymentRequestBuilder WithCardNumber(string cardNumber)
            {
                this.cardNumber = cardNumber;
                return this;
            }
            
            public PaymentRequestBuilder WithExpiryMonth(int expiryMonth)
            {
                this.expiryMonth = expiryMonth;
                return this;
            }
            
            public PaymentRequestBuilder WithExpiryDate(int expiryDate)
            {
                this.expiryDate = expiryDate;
                return this;
            }
            
            public PaymentRequestBuilder WithName(string name)
            {
                this.name = name;
                return this;
            }
            
            public PaymentRequestBuilder WithAmount(decimal amount)
            {
                this.amount = amount;
                return this;
            }
            
            public PaymentRequestBuilder WithCurrencyCode(string currencyCode)
            {
                this.currencyCode = currencyCode;
                return this;
            }
            
            public PaymentRequestBuilder WithCvv(int cvv)
            {
                this.cvv = cvv;
                return this;
            }

            public PaymentRequest Build() => new PaymentRequest
            {
                CardNumber = cardNumber,
                ExpiryMonth = expiryMonth,
                ExpiryDate = expiryDate,
                Name = name,
                Amount = amount,
                CurrencyCode = currencyCode,
                Cvv = cvv
            };
        }
    }
}