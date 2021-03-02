namespace PaymentGateway.Models
{
    /// <summary>
    /// Defines a payment response.
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Gets the payment identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets a value indicating whether the payment was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets the payment processing error.
        /// </summary>
        public string Error { get; set; }
    }
}