using System;
using FluentValidation;

namespace API.Models
{
    public sealed class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            EnsureInstanceNotNull(this);
            
            var currentYear = DateTime.UtcNow.Year;
            // double check what validation
            // can be put in place
            RuleFor(r => r.CardNumber)
                .NotEmpty();
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

            // double check what validation
            // can be put in place
            RuleFor(r => r.Cvv)
                .InclusiveBetween(100, 999);
        }
    }
}