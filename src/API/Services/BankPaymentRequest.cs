namespace API.Services
{
    public class BankPaymentRequest
    {
        public string CardNumber { get; set; }


        public int ExpiryMonth { get; set; }


        public int ExpiryDate { get; set; }


        public string Name { get; set; }


        public decimal Amount { get; set; }


        public string CurrencyCode { get; set; }


        public int Cvv { get; set; }
    }
}