namespace API.Services
{
    public sealed class AcquiringBankPaymentResponse
    {
        public string BankIdentifier { get; }
        
        public string PaymentStatus { get; }
        
        public AcquiringBankPaymentResponse(string bankIdentifier, string paymentStatus)
        {
            BankIdentifier = bankIdentifier;
            PaymentStatus = paymentStatus;
        }
    }
}