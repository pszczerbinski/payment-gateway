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

        public PaymentRequest(
            string cardNumber,
            string expirationDate,
            string cardVerificationValue,
            double amount,
            Currency currency)
        {
            this.CardNumber = cardNumber;
            this.ExpirationDate = expirationDate;
            this.CardVerificationValue = cardVerificationValue;
            this.Amount = amount;
            this.Currency = currency;
        }

        internal PaymentRequest()
        {
        }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public string CardVerificationValue { get; set; }

        public double Amount { get; set; }

        [JsonConverter(typeof(JsonCurrencyConverter))]
        public Currency Currency { get; set; }

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

            if (string.IsNullOrWhiteSpace(this.ExpirationDate) || !this.ValidateExirationDateFormat())
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
            return Regex.Match(this.ExpirationDate, ExpirationDateRegex).Success;
        }

        private bool ValidateExpirationDate()
        {
            var currentDate = DateTime.Now;
            var expirationDate = DateTime.ParseExact(this.ExpirationDate, "MM/yyyy", CultureInfo.InvariantCulture);
            if (expirationDate.Month >= currentDate.Month &&
                expirationDate.Year >= currentDate.Year)
            {
                return true;
            }
            return false;
        }
    }
}
