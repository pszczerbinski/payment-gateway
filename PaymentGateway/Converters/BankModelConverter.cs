namespace PaymentGateway.Converters
{
    using Bank;
    using global::PaymentGateway.Models;

    /// <summary>
    /// Defines a converter class used to convert payment gateway models to bank models.
    /// </summary>
    public static class BankModelConverter
    {
        /// <summary>
        /// Converts the <see cref="PaymentRequest"/> into a <see cref="BankPaymentRequest"/>.
        /// </summary>
        /// <param name="request"><see cref="PaymentRequest"/> instance.</param>
        /// <returns><see cref="BankPaymentRequest"/> instance.</returns>
        public static BankPaymentRequest From(PaymentRequest request)
        {
            return new BankPaymentRequest
            {
                CardNumber = request.CardNumber,
                CardExpirationDate = request.CardExpirationDate,
                CardVerificationValue = request.CardVerificationValue,
                CardHolderName = request.CardHolderName,
                Amount = request.Amount,
                Currency = request.Currency.Code,
            };
        }

        /// <summary>
        /// Converts the <see cref="BankPaymentResponse"/> into a <see cref="PaymentResponse"/>.
        /// </summary>
        /// <param name="request"><see cref="BankPaymentResponse"/> instance.</param>
        /// <returns><see cref="PaymentResponse"/> instance.</returns>
        public static PaymentResponse To(BankPaymentResponse response)
        {
            return new PaymentResponse
            {
                Identifier = response.Identifier,
                Success = response.Success,
                Error = response.Error,
            };
        }
    }
}
