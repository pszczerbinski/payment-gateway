namespace PaymentGateway.Models
{
    public class PaymentResponse
    {
        /// <summary>
        /// Gets the payment identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets a value indicating whether the payment was successful.
        /// </summary>
        public bool Success => this.Error == null;

        /// <summary>
        /// Gets the payment processing error.
        /// </summary>
        public string Error { get; set; }
    }
}