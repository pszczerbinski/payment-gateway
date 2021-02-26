namespace PaymentGateway.Models
{
    using System.Text.Json.Serialization;
    using PaymentGateway.Converters;

    /// <summary>
    /// Defines the payment request.
    /// </summary>
    public class PaymentRequest
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
        /// Gets the payment amount.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets the payment currency.
        /// </summary>
        [JsonConverter(typeof(JsonCurrencyConverter))]
        public Currency Currency { get; set; }
    }
}
