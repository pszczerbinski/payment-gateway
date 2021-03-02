namespace PaymentGateway.Converters
{
    using global::PaymentGateway.Models;
    using global::PaymentGateway.Storage;

    /// <summary>
    /// Defines a MongoDB model converter.
    /// </summary>
    public static class MongoModelConverter
    {
        /// <summary>
        /// Converts the <see cref="PaymentTransaction"/> to a <see cref="PaymentDetails"/>.
        /// </summary>
        /// <param name="transaction"><see cref="PaymentTransaction"/> instance.</param>
        /// <returns><see cref="PaymentDetails"/> instance.</returns>
        public static PaymentDetails From(PaymentTransaction transaction)
        {
            if (transaction == null)
            {
                return null;
            }

            return new PaymentDetails
            {
                Success = transaction.Success,
                Identifier = transaction.PaymentIdentifier,
                MaskedCardNumber = transaction.MaskedCardNumber,
                Error = transaction.Error,
                Amount = transaction.Amount,
                Currency = transaction.Currency,
            };
        }

        /// <summary>
        /// Converts the <see cref="PaymentRequest"/> and <see cref="PaymentResponse"/> to a <see cref="PaymentTransaction"/>.
        /// </summary>
        /// <param name="request"><see cref="PaymentRequest"/> instance.</param>
        /// <param name="response"><see cref="PaymentResponse"/> instance.</param>
        /// <returns><see cref="PaymentTransaction"/> instance.</returns>
        public static PaymentTransaction To(PaymentRequest request, PaymentResponse response)
        {
            if (request == null || response == null)
            {
                return null;
            }

            return new PaymentTransaction
            {
                Success = response.Success,
                PaymentIdentifier = response.Identifier,
                MaskedCardNumber = $"************{request.CardNumber[12..]}",
                Error = response.Error,
                Amount = request.Amount,
                Currency = request.Currency.Code,
            };
        }
    }
}
