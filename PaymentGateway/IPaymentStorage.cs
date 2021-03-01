namespace PaymentGateway
{
    using global::PaymentGateway.Models;

    public interface IPaymentStorage
    {
        public bool SavePaymentDetails(PaymentRequest request, PaymentResponse response);

        public PaymentDetails RetrievePaymentDetails(string identifier);
    }
}
