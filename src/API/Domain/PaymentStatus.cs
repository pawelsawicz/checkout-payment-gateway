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

        public string CurrencyCode { get; set; }
    }
}