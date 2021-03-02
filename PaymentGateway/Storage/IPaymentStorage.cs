namespace PaymentGateway.Storage
{
    using global::PaymentGateway.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the payment storage layer.
    /// </summary>
    public interface IPaymentStorage
    {
        /// <summary>
        /// Saves the payment details to the underlying storage.
        /// </summary>
        /// <param name="request">Payment request to be stored.</param>
        /// <param name="response">Payment response to be stored.</param>
        /// <returns>Whether the save operation was successful.</returns>
        public Task<bool> SavePaymentDetailsAsync(PaymentRequest request, PaymentResponse response);

        /// <summary>
        /// Retrieves a previous transaction from the storage.
        /// </summary>
        /// <param name="identifier">Transaction identifier to retrieve.</param>
        /// <returns>A task containing the payment details, or null if not found.</returns>
        public Task<PaymentDetails> RetrievePaymentDetailsAsync(string identifier);
    }
}
