namespace API.Services
{
    public sealed class AcquiringBankPaymentResponse
    {
        public string BankIdentifier { get; set; }
        
        public string PaymentStatus { get; set; }
    }
}