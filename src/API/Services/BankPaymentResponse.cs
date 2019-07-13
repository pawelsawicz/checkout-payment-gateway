namespace API.Services
{
    public sealed class BankPaymentResponse
    {
        public string BankIdentifier { get; set; }
        
        public string PaymentStatus { get; set; }
    }
}