namespace PaymentGateway
{
    using System.Threading.Tasks;
    using global::PaymentGateway.Models;

    /// <summary>
    /// Defines the signature for the payment gateway layer.
    /// </summary>
    public interface IPaymentGateway
    {
        /// <summary>
        /// Processes a payment request.
        /// </summary>
        /// <param name="request">Details of the payment to process.</param>
        /// <returns>A task containing the payment processing result.</returns>
        public Task<PaymentResponse> ProcessPaymentRequest(PaymentRequest request);
    }
}
