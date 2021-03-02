namespace PaymentGateway.Models
{
    /// <summary>
    /// Defines a payment response.
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the payment was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the payment processing error.
        /// </summary>
        public string Error { get; set; }
    }
}