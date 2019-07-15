using System;

namespace API.Domain
{
    public sealed class PaymentStatus
    {
        public string BankIdentifier { get; }

        public string PaymentStatusCode { get; }

        public string CardNumber { get; private set; }

        public int ExpiryMonth { get; }

        public int ExpiryDate { get; }

        public string Name { get; }

        public decimal Amount { get; }

        public string Currency { get; }

        public PaymentStatus(
            string bankIdentifier,
            string paymentStatusCode,
            string cardNumber,
            int expiryMonth,
            int expiryDate,
            string name,
            decimal amount,
            string currency)
        {
            BankIdentifier = bankIdentifier;
            PaymentStatusCode = paymentStatusCode;
            CardNumber = cardNumber;
            ExpiryMonth = expiryMonth;
            ExpiryDate = expiryDate;
            Name = name;
            Amount = amount;
            Currency = currency;
        }

        public PaymentStatus Mask()
        {
            var countToMask = CardNumber.Length - 4;
            var firstPart = new String('*', countToMask);
            var lastPart = CardNumber.Substring(countToMask, 4);
            CardNumber = firstPart + lastPart;
            return this;
        }
    }
}