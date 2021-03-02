namespace PaymentGateway.Models
{
    using System.Text.Json.Serialization;
    using global::PaymentGateway.Converters;

    /// <summary>
    /// Defines the payment request.
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the card expiration date.
        /// </summary>
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the card verification value.
        /// </summary>
        public string CardVerificationValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the card holder.
        /// </summary>
        public string CardHolderName { get; set; }

        /// <summary>
        /// Gets or sets the payment amount.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the payment currency.
        /// </summary>
        [JsonConverter(typeof(JsonCurrencyConverter))]
        public Currency Currency { get; set; }
    }
}
