namespace Bank
{
    /// <summary>
    /// Defines the bank payment request.
    /// </summary>
    public class BankPaymentRequest
    {
        /// <summary>
        /// Gets the card number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets the card expiration date.
        /// </summary>
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Gets the card verification value.
        /// </summary>
        public string CardVerificationValue { get; set; }

        /// <summary>
        /// Gets the name of the card holder.
        /// </summary>
        public string CardHolderName { get; set; }

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
