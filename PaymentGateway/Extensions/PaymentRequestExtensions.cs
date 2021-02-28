namespace PaymentGateway.Extensions
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using global::PaymentGateway.Models;

    /// <summary>
    /// Extension methods for the <see cref="PaymentRequest"/> class.
    /// </summary>
    public static class PaymentRequestExtensions
    {
        private const string CardNumberRegex = @"[\-\s]?[0-9]{16}$";
        private const string CvvRegex = @"^\d{3}$";
        private const string ExpirationDateRegex = @"^(0[1-9]{1}|1[0-2]{1})\/[0-9]{4}$";

        /// <summary>
        /// Validates the payment request.
        /// </summary>
        /// <returns>Validation result.</returns>
        public static PaymentRequestValidationResult Validate(this PaymentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CardNumber) || !ValidateCardNumber(request))
            {
                return PaymentRequestValidationResult.InvalidCardNumber;
            }

            if (string.IsNullOrWhiteSpace(request.CardVerificationValue) || !ValidateCvv(request))
            {
                return PaymentRequestValidationResult.InvalidCvv;
            }

            if (string.IsNullOrWhiteSpace(request.CardExpirationDate) || !ValidateExirationDateFormat(request))
            {
                return PaymentRequestValidationResult.InvalidExpirationDate;
            }
            else if (!ValidateExpirationDate(request))
            {
                return PaymentRequestValidationResult.CardExpired;
            }

            if (request.Amount < 0)
            {
                return PaymentRequestValidationResult.InvalidAmount;
            }

            if (request.Currency == null)
            {
                return PaymentRequestValidationResult.InvalidCurrency;
            }

            if (string.IsNullOrWhiteSpace(request.CardHolderName))
            {
                return PaymentRequestValidationResult.InvalidCardHolderName;
            }

            return PaymentRequestValidationResult.Success;
        }

        private static bool ValidateCardNumber(PaymentRequest request)
        {
            return Regex.Match(request.CardNumber, CardNumberRegex).Success;
        }

        private static bool ValidateCvv(PaymentRequest request)
        {
            return Regex.Match(request.CardVerificationValue, CvvRegex).Success;
        }

        private static bool ValidateExirationDateFormat(PaymentRequest request)
        {
            // Expiration date must be in MM/yyyy format
            return Regex.Match(request.CardExpirationDate, ExpirationDateRegex).Success;
        }

        private static bool ValidateExpirationDate(PaymentRequest request)
        {
            var currentDate = DateTime.Now;
            var expirationDate = DateTime.ParseExact(request.CardExpirationDate, "MM/yyyy", CultureInfo.InvariantCulture);
            if (expirationDate.Month >= currentDate.Month &&
                expirationDate.Year >= currentDate.Year)
            {
                return true;
            }
            return false;
        }
    }
}
