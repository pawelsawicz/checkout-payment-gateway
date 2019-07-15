namespace API.Services
{
    public sealed class AcquiringBankPaymentRequest
    {
        public string CardNumber { get; }


        public int ExpiryMonth { get; }


        public int ExpiryDate { get; }


        public string Name { get; }


        public decimal Amount { get; }


        public string CurrencyCode { get; }


        public int Cvv { get; }
        
        public AcquiringBankPaymentRequest(
            string cardNumber,
            int expiryMonth,
            int expiryDate,
            string name,
            decimal amount,
            string currencyCode,
            int cvv)
        {
            CardNumber = cardNumber;
            ExpiryMonth = expiryMonth;
            ExpiryDate = expiryDate;
            Name = name;
            Amount = amount;
            CurrencyCode = currencyCode;
            Cvv = cvv;
        }
    }
}