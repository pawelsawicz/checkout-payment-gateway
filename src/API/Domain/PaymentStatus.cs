using System;

namespace API.Domain
{
    public sealed class PaymentStatus
    {
        public string BankIdentifier { get; set; }
        
        public string PaymentStatusCode { get; set; }
        
        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryDate { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

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