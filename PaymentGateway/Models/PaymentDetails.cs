namespace PaymentGateway.Models
{
    public class PaymentDetails
    {
        /// <summary>
        /// Gets the payment identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets the masked card number that was used in the payment.
        /// </summary>
        public string MaskedCardNumber { get; set; }

        /// <summary>
        /// Gets a value indicating whether the payment was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets the payment error.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets the payment amount.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets the payment currency.
        /// </summary>
        public string Currency { get; set; }
    }
}
