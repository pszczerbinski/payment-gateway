namespace PaymentGateway.Models
{
    using PaymentGateway.Converters;
    using System;
    using System.Globalization;
    using System.Text.Json.Serialization;
    using System.Text.RegularExpressions;

    public class PaymentRequest
    {
        private const string CardNumberRegex = @"[\-\s]?[0-9]{16}$";
        private const string CvvRegex = @"^\d{3}$";
        private const string ExpirationDateRegex = @"^(0[1-9]{1}|1[0-2]{1})\/[0-9]{4}$";

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRequest"/> class.
        /// </summary>
        /// <param name="cardNumber">Card number for the payment.</param>
        /// <param name="cardExpirationDate">Card expiry date in MM/yyyy format.</param>
        /// <param name="cardVerificationValue">Card verification value.</param>
        /// <param name="amount">Payment amount.</param>
        /// <param name="currency">Payment currency.</param>
        public PaymentRequest(
            string cardNumber,
            string cardExpirationDate,
            string cardVerificationValue,
            double amount,
            Currency currency)
        {
            this.CardNumber = cardNumber;
            this.CardExpirationDate = cardExpirationDate;
            this.CardVerificationValue = cardVerificationValue;
            this.Amount = amount;
            this.Currency = currency;
        }

        internal PaymentRequest()
        {
        }

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

        /// <summary>
        /// Validates the payment request.
        /// </summary>
        /// <returns>Validation result.</returns>
        public PaymentRequestValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(this.CardNumber) || !this.ValidateCardNumber())
            {
                return PaymentRequestValidationResult.InvalidCardNumber;
            }

            if (string.IsNullOrWhiteSpace(this.CardVerificationValue) || !this.ValidateCvv())
            {
                return PaymentRequestValidationResult.InvalidCvv;
            }

            if (string.IsNullOrWhiteSpace(this.CardExpirationDate) || !this.ValidateExirationDateFormat())
            {
                return PaymentRequestValidationResult.InvalidExpirationDate;
            }
            else if (!this.ValidateExpirationDate())
            {
                return PaymentRequestValidationResult.CardExpired;
            }

            if (this.Amount < 0)
            {
                return PaymentRequestValidationResult.InvalidAmount;
            }

            if (this.Currency == null)
            {
                return PaymentRequestValidationResult.InvalidCurrency;
            }

            return PaymentRequestValidationResult.Success;
        }

        private bool ValidateCardNumber()
        {
            return Regex.Match(this.CardNumber, CardNumberRegex).Success;
        }

        private bool ValidateCvv()
        {
            return Regex.Match(this.CardVerificationValue, CvvRegex).Success;
        }

        private bool ValidateExirationDateFormat()
        {
            // Expiration date must be in MM/yyyy format
            return Regex.Match(this.CardExpirationDate, ExpirationDateRegex).Success;
        }

        private bool ValidateExpirationDate()
        {
            var currentDate = DateTime.Now;
            var expirationDate = DateTime.ParseExact(this.CardExpirationDate, "MM/yyyy", CultureInfo.InvariantCulture);
            if (expirationDate.Month >= currentDate.Month &&
                expirationDate.Year >= currentDate.Year)
            {
                return true;
            }
            return false;
        }
    }
}
