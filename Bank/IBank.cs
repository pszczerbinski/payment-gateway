namespace Bank
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the bank operations.
    /// </summary>
    public interface IBank
    {
        /// <summary>
        /// Processes a payment request.
        /// </summary>
        /// <param name="request">Details of the payment to process.</param>
        /// <returns>A task containing the payment processing result.</returns>
        public Task<BankPaymentResponse> ProcessPayment(BankPaymentRequest request);
    }
}
