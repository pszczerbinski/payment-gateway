namespace PaymentGateway.Models
{
    /// <summary>
    /// Defines the payment details.
    /// </summary>
    public class PaymentDetails
    {
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the masked card number that was used in the payment.
        /// </summary>
        public string MaskedCardNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the payment was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the payment error.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the payment amount.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the payment currency.
        /// </summary>
        public string Currency { get; set; }
    }
}
