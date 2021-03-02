namespace Bank
{
    public class BankPaymentResponse
    {
        /// <summary>
        /// Gets the payment identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets a value indicating whether payment was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets the payment processing error.
        /// </summary>
        public string Error { get; set; }
    }
}