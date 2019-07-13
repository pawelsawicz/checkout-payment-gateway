using System.ComponentModel.DataAnnotations;

namespace API
{
    public sealed class PaymentRequest
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        public int ExpiryMonth { get; set; }
        
        [Required]
        public int ExpiryDate { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public int Cvv { get; set; }
    }
}