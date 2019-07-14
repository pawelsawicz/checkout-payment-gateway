using System;
using CreditCardValidator;
using FluentValidation;
using FluentValidation.Validators;

namespace API.Models
{
    public sealed class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            EnsureInstanceNotNull(this);
            
            var currentYear = DateTime.UtcNow.Year;

            RuleFor(r => r.CardNumber)
                .NotEmpty()
                .SetValidator(new CardNumberValidator("Card Number is not valid"));
            RuleFor(r => r.ExpiryMonth)
                .NotEmpty()
                .InclusiveBetween(1, 12);
            RuleFor(r => r.ExpiryDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(currentYear);
            RuleFor(r => r.Name)
                .NotEmpty();
            RuleFor(r => r.Amount)
                .NotEmpty()
                .GreaterThan(0);

            // double check what validation
            // can be put in place
            RuleFor(r => r.CurrencyCode)
                .NotEmpty();

            // According to https://medium.com/hootsuite-engineering/a-comprehensive-guide-to-validating-and-formatting-credit-cards-b9fa63ec7863
            // Most cards have 3 digits, and some 4.
            RuleFor(r => r.Cvv)
                .InclusiveBetween(100, 9999);
        }

        private class CardNumberValidator : PropertyValidator
        {
            public CardNumberValidator(string errorMessage) : base(errorMessage)
            {
            }

            protected override bool IsValid(PropertyValidatorContext context)
            {
                var cardNumber = context.PropertyValue as string;
                if (string.IsNullOrEmpty(cardNumber))
                {
                    return false;
                }
                var detector = new CreditCardDetector(cardNumber);
                return detector.IsValid();
            }
        }
    }
}